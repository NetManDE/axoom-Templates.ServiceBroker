using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OpenServiceBroker.Errors;
using OpenServiceBroker.Instances;
using Xunit;

namespace MyVendor.ServiceBroker.Broker
{
    public class ServiceInstanceServiceFacts : DatabaseFactsBase<ServiceInstanceService>
    {
        public ServiceInstanceServiceFacts()
        {
            Use(Options.Create(new BrokerOptions {Catalog = Fake.Catalog}));
        }

        [Fact]
        public async Task Provisions()
        {
            var result = await Subject.ProvisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceProvisionRequest
                {
                    ServiceId = "myservice1",
                    PlanId = "myplan1a",
                    Parameters = new JObject
                    {
                        ["key"] = "value"
                    }
                });

            result.Unchanged.Should().BeFalse();

            Context.ServiceInstances.Should().BeEquivalentTo(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice1",
                PlanId = "myplan1a",
                Parameters = "{\"key\":\"value\"}"
            });
        }

        [Fact]
        public async Task ProvisionRejectsUnknownServiceId()
        {
            await Subject.Awaiting(x => x.ProvisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceProvisionRequest
                {
                    ServiceId = "unknownservice",
                    PlanId = "unknownplan"
                })).Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task ProvisionRejectsUnknownPlanId()
        {
            await Subject.Awaiting(x => x.ProvisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceProvisionRequest
                {
                    ServiceId = "myservice1",
                    PlanId = "unknownplan"
                })).Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task ProvisionAllowsExactDuplicates()
        {
            AddDetached(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice1",
                PlanId = "myplan1a"
            });

            var result = await Subject.ProvisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceProvisionRequest
                {
                    ServiceId = "myservice1",
                    PlanId = "myplan1a"
                });

            result.Unchanged.Should().BeTrue();
        }

        [Fact]
        public async Task ProvisionRejectsNonEqualDuplicates()
        {
            AddDetached(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice1",
                PlanId = "myplan1a"
            });

            await Subject.Awaiting(x => x.ProvisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceProvisionRequest
                {
                    ServiceId = "myservice1",
                    PlanId = "myplan1a",
                    Parameters = new JObject {["key"] = "value"}
                })).Should().ThrowAsync<ConflictException>();
        }

        [Fact]
        public async Task Updates()
        {
            AddDetached(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice1",
                PlanId = "myplan1a",
                Parameters = "{\"key\":\"value\"}"
            });

            await Subject.UpdateAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceUpdateRequest
                {
                    ServiceId = "myservice1",
                    PlanId = "myplan1b",
                    Parameters = new JObject {["key"] = "value2"}
                });

            Context.ServiceInstances.Should().BeEquivalentTo(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice1",
                PlanId = "myplan1b",
                Parameters = "{\"key\":\"value2\"}"
            });
        }

        [Fact]
        public async Task UpdateRejectsChangingServiceId()
        {
            AddDetached(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice1",
                PlanId = "myplan1a"
            });

            await Subject.Awaiting(x => x.UpdateAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceUpdateRequest
                {
                    ServiceId = "myservice2"
                })).Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task UpdateRejectsChangingPlanIdWhenNotAllowed()
        {
            AddDetached(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice2",
                PlanId = "myplan2a"
            });

            await Subject.Awaiting(x => x.UpdateAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                new ServiceInstanceUpdateRequest
                {
                    ServiceId = "myservice2",
                    PlanId = "myplan2B"
                })).Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task Deprovisions()
        {
            AddDetached(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice1",
                PlanId = "myplan1a"
            });

            await Subject.DeprovisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                serviceId: "myservice1",
                planId: "myplan1a");

            Context.ServiceInstances.Should().BeEmpty();
        }

        [Fact]
        public async Task DeprovisionReportsGoneForMissingInstance()
        {
            await Subject.Awaiting(x => x.DeprovisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                serviceId: "myservice1",
                planId: "myplan1a")).Should().ThrowAsync<GoneException>();
        }

        [Fact]
        public async Task DeprovisionRejectsIncorrectPlanId()
        {
            AddDetached(new ServiceInstanceEntity
            {
                Id = "abc123",
                ServiceId = "myservice1",
                PlanId = "myplan1a"
            });

            await Subject.Awaiting(x => x.DeprovisionAsync(
                new ServiceInstanceContext(instanceId: "abc123"),
                serviceId: "myservice1",
                planId: "myplan1b")).Should().ThrowAsync<BadRequestException>();
        }
    }
}
