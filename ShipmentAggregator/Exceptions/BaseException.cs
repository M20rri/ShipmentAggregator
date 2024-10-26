namespace ShipmentAggregator.Exceptions;
public abstract class BaseException : ApplicationException
{
    protected BaseException(int code, string message, string? contextName = null, dynamic? contextValue = null, IReadOnlyDictionary<string, string[]>? errors = null) : base(message)
    {
        Code = code;
        Message = message;
        ContextName = contextName;
        ContextValue = contextValue;
        Errors = errors;
    }

    public int Code { get; }
    public new string Message { get; }
    public string? ContextName { get; }
    public dynamic? ContextValue { get; }
    public IReadOnlyDictionary<string, string[]>? Errors { get; protected set; }
}
