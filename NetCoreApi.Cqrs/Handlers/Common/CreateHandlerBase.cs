using System;
using System.Threading.Tasks;
using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Handlers.Common
{
    public abstract class CreateHandlerBase<TRequest, TResult, TModel> : IHandler<TRequest, Task<TResult>>
        where TModel : EntityBase
    {
        private readonly ICommand<TRequest, TModel> _command;
        private readonly IMapper _mapper;

        public CreateHandlerBase(ICommand<TRequest, TModel> command, IMapper mapper)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TResult> Handle(TRequest request)
        {
            var item = await _command.ExecuteAsync(request);

            return _mapper.Map<TResult>(item);
        }
    }
}
