namespace NetCoreApi.Cqrs.Common
{
    /// <summary>
    /// Query cannot change state.
    /// Contains data access logic.
    /// </summary>
    /// <typeparam name="TParameter">Parameter type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    public interface IQuery<in TParameter, TResult>
    {
        TResult Execute(TParameter parameter);
    }

    public interface IQuery<TResult>
    {
        TResult Execute();
    }
}
