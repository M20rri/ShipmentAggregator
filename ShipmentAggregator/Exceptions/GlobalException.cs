namespace ShipmentAggregator.Exceptions;

public class GlobalException(string message, string? contextName = null, object? contextValue = null)
    : BaseException(StatusCodes.Status500InternalServerError, message, contextName, contextValue);

