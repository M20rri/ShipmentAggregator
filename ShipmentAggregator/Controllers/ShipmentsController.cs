namespace ShipmentAggregator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShipmentsController : ControllerBase
{
    private readonly DeliveryCompanyServiceFactory _factory;
    private readonly ILogger<ShipmentsController> _logger;

    public ShipmentsController(DeliveryCompanyServiceFactory factory, ILogger<ShipmentsController> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateShipment([FromBody] ShipmentRequest request)
    {
        var service = _factory.GetDeliveryService(request.Company);
        var response = await service.CreateShipment(request);
        return Ok(response);
    }

    [HttpGet("status/{shipmentId}")]
    public async Task<IActionResult> GetShipmentStatus(string shipmentId)
    {
        // Determine the company from the shipmentId or have another mechanism to retrieve it
        var company = DeliveryCompany.CompanyA; // Example placeholder
        var service = _factory.GetDeliveryService(company);
        var response = await service.GetShipmentStatus(shipmentId);
        return Ok(response);
    }
}
