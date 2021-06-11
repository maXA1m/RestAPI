using System;
using System.Threading.Tasks;
using AutoMapper;
using NetCoreApi.Common.Exceptions;
using NetCoreApi.Cqrs.Common;
using NetCoreApi.Data.Model;

namespace NetCoreApi.Cqrs.Handlers.Common
{
    public abstract class DeleteByIdHandlerBase<TResult, TModel> : IHandler<int, Task<TResult>>
        where TResult : class
        where TModel : EntityBase
    {
        private readonly ICommand<int, TModel> _command;
        private readonly IMapper _mapper;

        public DeleteByIdHandlerBase(ICommand<int, TModel> command, IMapper mapper)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TResult> Handle(int request)
        {
            TModel item;
            try
            {
                item = await _command.ExecuteAsync(request);
                
            }
            catch (NotFoundException)
            {
                return null;
            }

            return item == null ? null : _mapper.Map<TResult>(item);
        }
    }
}
