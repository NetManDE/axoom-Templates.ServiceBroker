using System.ComponentModel.DataAnnotations;

namespace MyVendor.ServiceBroker.Broker
{
    public class ServiceInstanceEntity
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ServiceId { get; set; }

        [Required]
        public string PlanId { get; set; }

        public string Parameters { get; set; }
    }
}
