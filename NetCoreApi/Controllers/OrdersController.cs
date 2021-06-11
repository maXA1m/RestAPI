using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NetCoreApi.Data.Model;
using NetCoreApi.Filters;
using NetCoreApi.Model;
using NetCoreApi.Model.Orders;
using NetCoreApi.Queries.Common;

namespace NetCoreApi.Controllers
{
    /// <summary>
    /// Provides API to access orders.
    /// Classic N-Layer with Services/Managers needs to inject many dependencies.
    /// </summary>
    [ApiController]
    [Route("api/v1/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IQueryProcessor<User, CreateOrderModel, UpdateOrderModel> _query;

        public OrdersController(IQueryProcessor<User, CreateOrderModel, UpdateOrderModel> query, IMemoryCache cache, IMapper mapper)
        {
            _query = query;
            _cache = cache;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<OrderModel> Get([FromQuery] PaginationOptions paginaton = null)
        {
            var key = paginaton == null ? "orders" : $"orders{paginaton.Offset}_{paginaton.Count}";
            if (!_cache.TryGetValue(key, out IEnumerable<OrderModel> items))
            {
                items = _query.Get(paginaton).ProjectTo<OrderModel>(_mapper.ConfigurationProvider);

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                _cache.Set(key, items, cacheOptions);
            }

            return items;
        }

        [HttpGet("{id}")]
        public OrderModel Get(int id)
        {
            var key = $"order{id}";
            if (!_cache.TryGetValue(key, out OrderModel item))
            {
                item = _mapper.Map<OrderModel>(_query.Get(id));

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                _cache.Set(key, item, cacheOptions);
            }

            return item;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<OrderModel> Post([FromBody] CreateOrderModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<OrderModel>(item);

            return model;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<OrderModel> Put(int id, [FromBody] UpdateOrderModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<OrderModel>(item);

            // Update cache.
            _cache.Set($"order{id}", model);

            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            // Delete from main storage.
            await _query.Delete(id);

            // Remove from cache.
            _cache.Remove($"order{id}");
        }
    }
}
