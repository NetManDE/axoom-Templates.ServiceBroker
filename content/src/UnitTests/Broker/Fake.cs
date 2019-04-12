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
                    Id = "myservice1",
                    Name = "myservice1",
                    PlanUpdateable = true,
                    Plans =
                    {
                        new Plan
                        {
                            Id = "myplan1a",
                            Name = "myplan1a"
                        },
                        new Plan
                        {
                            Id = "myplan1b",
                            Name = "myplan1b"
                        }
                    }
                },
                new Service
                {
                    Id = "myservice2",
                    Name = "myservice2",
                    PlanUpdateable = false,
                    Plans =
                    {
                        new Plan
                        {
                            Id = "myplan2a",
                            Name = "myplan2a"
                        },
                        new Plan
                        {
                            Id = "myplan2B",
                            Name = "myplan2B"
                        }
                    }
                }
            }
        };
    }
}
