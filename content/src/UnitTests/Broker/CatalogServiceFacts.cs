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
            var catalog = new Catalog
            {
                Services =
                {
                    new Service
                    {
                        Id = "myservice",
                        Name = "myservice",
                        Plans =
                        {
                            new Plan
                            {
                                Id = "myplan",
                                Name = "myplan"
                            }
                        }
                    }
                }
            };
            Use(Options.Create(new BrokerOptions {Catalog = catalog}));

            var result = await Subject.GetCatalogAsync();
            result.Should().Be(catalog);
        }
    }
}
