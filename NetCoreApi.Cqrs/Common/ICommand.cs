using System.Threading.Tasks;

namespace NetCoreApi.Cqrs.Common
{
    /// <summary>
    /// Command can change state and return something.
    /// Contains data access logic.
    /// </summary>
    /// <typeparam name="TParameter">Parameter type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    public interface ICommand<in TParameter, TResult>
    {
        Task<TResult> ExecuteAsync(TParameter parameter);
    }

    public interface ICommand<in TParameter>
    {
        Task ExecuteAsync(TParameter parameter);
    }
}
