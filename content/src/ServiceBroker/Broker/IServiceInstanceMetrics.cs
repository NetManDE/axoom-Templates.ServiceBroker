namespace MyVendor.ServiceBroker.Broker
{
    public interface IServiceInstanceMetrics
    {
        void Provisioned(string serviceId);
        void Updated(string serviceId);
        void Deprovisioned();
    }
}
