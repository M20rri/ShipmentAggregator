namespace ShipmentAggregator.Interfaces;
public interface IDeliveryService
{
    Task<ShipmentResponse> CreateShipment(ShipmentRequest request);
    Task<ShipmentStatusResponse> GetShipmentStatus(string shipmentId);
}
