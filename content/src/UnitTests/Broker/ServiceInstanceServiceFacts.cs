using System.Threading.Tasks;
using OpenServiceBroker.Instances;
using Xunit;

namespace MyVendor.ServiceBroker.Broker
{
    public class ServiceInstanceServiceFacts : AutoMockingFactsBase<ServiceInstanceService>
    {
        [Fact]
        public async Task Provisions()
        {
            var result = await Subject.ProvisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceProvisionRequest {ServiceId = "myservice", PlanId = "myplan"});
        }

        [Fact]
        public async Task Updates()
        {
            await Subject.UpdateAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceUpdateRequest());
        }

        [Fact]
        public async Task Deprovisions()
        {
            await Subject.DeprovisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                serviceId: "myservice",
                planId: "myplan");
        }
    }
}
