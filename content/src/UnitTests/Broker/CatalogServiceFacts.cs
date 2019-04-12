using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using OpenServiceBroker.Catalogs;
using Xunit;

namespace MyVendor.ServiceBroker.Broker
{
    public class CatalogServiceFacts : AutoMockingFactsBase<CatalogService>
    {
        [Fact]
        public async Task GetsCatalog()
        {
            Use(Options.Create(new BrokerOptions {Catalog = Fake.Catalog}));

            var result = await Subject.GetCatalogAsync();
            result.Should().BeEquivalentTo(Fake.Catalog);
        }
    }
}
