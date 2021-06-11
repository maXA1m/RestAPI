using System;
using System.Threading.Tasks;
using AutoMapper;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Handlers.Common
{
    public abstract class UpdateByIdHandlerBase<TRequest, TResult, TModel> : IHandler<(int Id, TRequest Model), Task<TResult>>
        where TModel : EntityBase
    {
        private readonly ICommand<(int Id, TRequest Model), TModel> _command;
        private readonly IMapper _mapper;

        public UpdateByIdHandlerBase(ICommand<(int Id, TRequest Model), TModel> command, IMapper mapper)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TResult> Handle((int Id, TRequest Model) request)
        {
            var item = await _command.ExecuteAsync(request);

            return _mapper.Map<TResult>(item);
        }
    }
}
