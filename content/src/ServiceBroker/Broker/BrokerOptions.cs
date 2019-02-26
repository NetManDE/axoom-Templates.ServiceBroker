using OpenServiceBroker.Catalogs;

namespace MyVendor.ServiceBroker.Broker
{
    public class BrokerOptions
    {
        public Catalog Catalog { get; set; } = new Catalog();
    }
}
