using Microsoft.EntityFrameworkCore;
using MyVendor.ServiceBroker.Broker;

// ReSharper disable once CheckNamespace
namespace MyVendor.ServiceBroker
{
    public partial class DbContext
    {
        public DbSet<ServiceInstanceEntity> ServiceInstances { get; set; }
    }
}
