using OpenServiceBroker.Catalogs;

namespace MyVendor.ServiceBroker.Broker
{
    public static class Fake
    {
        public static Catalog Catalog => new Catalog
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
    }
}
