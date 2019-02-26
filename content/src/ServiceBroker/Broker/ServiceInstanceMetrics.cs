using Nexogen.Libraries.Metrics;

namespace MyVendor.ServiceBroker.Broker
{
    public class ServiceInstanceMetrics : IServiceInstanceMetrics
    {
        private readonly ILabelledCounter _provisioned;
        private readonly ILabelledCounter _updated;
        private readonly ICounter _deprovisioned;

        public ServiceInstanceMetrics(IMetrics metrics)
        {
            _provisioned =
                metrics.Counter()
                       .Name("myvendor_servicebroker_serviceinstances_provisioned")
                       .Help("The number of service instances that were provisioned.")
                       .LabelNames("serviceId")
                       .Register();
            _updated =
                metrics.Counter()
                       .Name("myvendor_servicebroker_serviceinstances_updated")
                       .Help("The number of service instances that were updated.")
                       .LabelNames("serviceId")
                       .Register();
            _deprovisioned =
                metrics.Counter()
                       .Name("myvendor_servicebroker_serviceinstances_deprovisioned")
                       .Help("The number of service instances that were deprovisioned.")
                       .Register();
        }

        public void Provisioned(string serviceId)
            => _provisioned.Labels(serviceId).Increment();

        public void Updated(string serviceId)
            => _updated.Labels(serviceId).Increment();

        public void Deprovisioned()
            => _deprovisioned.Increment();
    }
}
