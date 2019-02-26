using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenServiceBroker.Catalogs;

namespace MyVendor.ServiceBroker.Broker
{
    public class CatalogService : ICatalogService
    {
        private readonly BrokerOptions _options;

        public CatalogService(IOptions<BrokerOptions> options)
        {
            _options = options.Value;
        }

        public Task<Catalog> GetCatalogAsync()
        {
            return Task.FromResult(_options.Catalog);
        }
    }
}
