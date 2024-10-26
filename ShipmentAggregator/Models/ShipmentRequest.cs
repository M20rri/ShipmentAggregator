namespace ShipmentAggregator.Models;

public class ShipmentRequest
{
    public string ShipmentCode { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string Address { get; set; }
    public DeliveryCompany Company { get; set; }
}
