namespace NetCoreApi.Cqrs.Common
{
    /// <summary>
    /// Handler for query or command.
    /// Conatains logic like mapping or caching.
    /// </summary>
    /// <typeparam name="TRequest">Request type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    public interface IHandler<TRequest, TResult>
    {
        TResult Handle(TRequest request);
    }
}
