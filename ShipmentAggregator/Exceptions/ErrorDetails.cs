namespace ShipmentAggregator.Exceptions;
public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string? ContextName { get; set; }
    public dynamic? ContextValue { get; set; }
    public IReadOnlyDictionary<string, string[]>? Errors { get; set; }

    public override string ToString()
        => JsonConvert.SerializeObject(this);
}
