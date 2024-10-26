namespace ShipmentAggregator.Factory;

public class DeliveryCompanyServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DeliveryCompanyServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDeliveryService GetDeliveryService(DeliveryCompany company)
    {
        return company switch
        {
            DeliveryCompany.CompanyA => _serviceProvider.GetService<CompanyAService>(),
            DeliveryCompany.CompanyB => _serviceProvider.GetService<CompanyBService>(),
            DeliveryCompany.CompanyC => _serviceProvider.GetService<CompanyCService>(),
            _ => throw new GlobalException("Invalid delivery company")
        };
    }
}
