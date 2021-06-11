using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Model;
using NetCoreApi.Model;

namespace NetCoreApi.Cqrs.Handlers.Common
{
    public abstract class GetAllHandlerBase<TResult, TModel> : IHandler<PaginationOptions, IQueryable<TResult>>
        where TModel : EntityBase
    {
        private readonly IQuery<PaginationOptions, IQueryable<TModel>> _query;
        private readonly IMapper _mapper;

        public GetAllHandlerBase(IQuery<PaginationOptions, IQueryable<TModel>> query, IMapper mapper)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IQueryable<TResult> Handle(PaginationOptions request)
        {
            var items = _query.Execute(request);

            return items.ProjectTo<TResult>(_mapper.ConfigurationProvider);
        }
    }
}
