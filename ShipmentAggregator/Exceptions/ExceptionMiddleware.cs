namespace ShipmentAggregator.Exceptions;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BaseException ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        if (exception is BaseException baseException)
        {
            context.Response.StatusCode = baseException.Code;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = baseException.Code,
                Message = baseException.Message,
                ContextName = baseException.ContextName,
                ContextValue = baseException.ContextValue,
                Errors = baseException.Errors
            }.ToString());
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Internal Server Error"
            }.ToString());
        }
    }
}
