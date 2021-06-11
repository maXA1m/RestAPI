using System;
using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Handlers.Common
{
    public abstract class GetByIdHandlerBase<TResult, TModel> : IHandler<int, TResult>
        where TModel : EntityBase
    {
        private readonly IQuery<int, TModel> _query;
        private readonly IMapper _mapper;

        public GetByIdHandlerBase(IQuery<int, TModel> query, IMapper mapper)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public TResult Handle(int request)
        {
            var item = _query.Execute(request);

            return _mapper.Map<TResult>(item);
        }
    }
}
