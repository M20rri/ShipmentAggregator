namespace ShipmentAggregator.Services;
public class CompanyAService(ICacheService cacheService) : IDeliveryService
{
    public async Task<ShipmentResponse> CreateShipment(ShipmentRequest request)
    {
        var isExist = await IsExistBefore(request.ShipmentCode);

        if (isExist)
            throw new GlobalException("This Shipment exists before");

        var shipment = JsonConvert.SerializeObject(request);

        await cacheService.SetAsync($"Code_${request.ShipmentCode}", shipment, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        });
        return await Task.FromResult(new ShipmentResponse { Message = $"{request.ShipmentCode} Created succesfully", Status = "Created" });
    }

    public async Task<ShipmentStatusResponse> GetShipmentStatus(string shipmentCode)
    {
        var isExist = await IsExistBefore(shipmentCode);

        if (!isExist)
            throw new GlobalException("This Shipment not found");

        var shipmentStr = await cacheService.GetAsync<string>($"Code_${shipmentCode}");
        var response = new ShipmentStatusResponse();
        if (!string.IsNullOrEmpty(shipmentStr))
        {
            var shipment = JsonConvert.DeserializeObject<ShipmentRequest>(shipmentStr)!;
            response = new ShipmentStatusResponse()
            {
                ShipmentCode = shipmentCode,
                Status = "In Transit A"
            };
        }
        return response;
    }

    private async Task<bool> IsExistBefore(string shipmentCode)
    {
        var shipmentStr = await cacheService.GetAsync<string>($"Code_${shipmentCode}");
        return !string.IsNullOrEmpty(shipmentStr);
    }
}
