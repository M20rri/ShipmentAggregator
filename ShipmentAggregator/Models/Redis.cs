namespace ShipmentAggregator.Models;
public class Redis
{
    public string? ConnectionString { get; set; }
    public string? Instance { get; set; }
    public int AbsoluteExpirationRelativeToNow { get; set; }
}
