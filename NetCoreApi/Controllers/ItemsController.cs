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
using NetCoreApi.Model.Items;
using NetCoreApi.Queries.Common;
using NetCoreApi.Security;

namespace NetCoreApi.Controllers
{
    /// <summary>
    /// Provides API to access items.
    /// Classic N-Layer with Services/Managers needs to inject many dependencies.
    /// </summary>
    [ApiController]
    [Route("api/v1/items")]
    [AllowAnonymous]
    public class ItemsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IQueryProcessor<Item, CreateItemModel, UpdateItemModel> _query;

        public ItemsController(IQueryProcessor<Item, CreateItemModel, UpdateItemModel> query, IMemoryCache cache, IMapper mapper)
        {
            _query = query;
            _cache = cache;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ItemModel> Get([FromQuery] PaginationOptions paginaton = null)
        {
            var key = paginaton == null ? "items" : $"items{paginaton.Offset}_{paginaton.Count}";
            if (!_cache.TryGetValue(key, out IEnumerable<ItemModel> items))
            {
                items = _query.Get(paginaton).ProjectTo<ItemModel>(_mapper.ConfigurationProvider);

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
        public ItemModel Get(int id)
        {
            var key = $"item{id}";
            if (!_cache.TryGetValue(key, out ItemModel item))
            {
                item = _mapper.Map<ItemModel>(_query.Get(id));

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
        [Authorize(Roles = Roles.AdministratorOrManager)]
        public async Task<ItemModel> Post([FromBody] CreateItemModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<ItemModel>(item);

            return model;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = Roles.AdministratorOrManager)]
        public async Task<ItemModel> Put(int id, [FromBody] UpdateItemModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<ItemModel>(item);

            // Update cache.
            _cache.Set($"item{id}", model);

            return model;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdministratorOrManager)]
        public async Task Delete(int id)
        {
            // Delete from main storage.
            await _query.Delete(id);

            // Remove from cache.
            _cache.Remove($"item{id}");
        }
    }
}
