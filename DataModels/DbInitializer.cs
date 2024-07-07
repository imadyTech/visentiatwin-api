using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VisentiaTwin_API.DataContexts;

namespace VisentiaTwin_API.DataModels
{
    public enum YB_RentalStatus
    {
        pending = 0,
        approved = 1,
        rejected = 2,
        completed = 3
    }
    public static class DbInitializer
    {
        public static void InitializeSystem(VTSystemContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there are any systems already present
            if (context.VTSystems.Any())
            {
                return; // DB has been seeded
            }

            // Create initial data
            var systems = new VTSystem[]
            {
            new VTSystem { Name = "System1", Description = "First system", Version = "1.0", Author = "Author1" },
            new VTSystem { Name = "System2", Description = "Second system", Version = "1.0", Author = "Author2" }
            };

            foreach (var s in systems)
            {
                context.VTSystems.Add(s);
            }
            context.SaveChanges();

            var components = new VTComponent[]
            {
            new VTComponent { Name = "Component1", Description = "First component", Version = "1.0", Author = "Author1", Cost = 100.0f },
            new VTComponent { Name = "Component2", Description = "Second component", Version = "1.0", Author = "Author2", Cost = 150.0f }
            };

            foreach (var c in components)
            {
                context.VTComponents.Add(c);
            }
            context.SaveChanges();

            var nodes = new VTNode[]
            {
            new VTNode { Name = "Node1", Description = "First node", Version = "1.0", Author = "Author1", VTSystem = systems[0] },
            new VTNode { Name = "Node2", Description = "Second node", Version = "1.0", Author = "Author2", VTSystem = systems[1] }
            };

            foreach (var n in nodes)
            {
                context.VTNodes.Add(n);
            }
            context.SaveChanges();

            var nodeComponents = new VTNodeComponent[]
            {
            new VTNodeComponent { VTNodeId = nodes[0].VTNodeId, VTComponentId = components[0].VTComponentId },
            new VTNodeComponent { VTNodeId = nodes[0].VTNodeId, VTComponentId = components[1].VTComponentId },
            new VTNodeComponent { VTNodeId = nodes[1].VTNodeId, VTComponentId = components[0].VTComponentId }
            };

            foreach (var nc in nodeComponents)
            {
                context.VTNodeComponents.Add(nc);
            }
            context.SaveChanges();
        }

        public static void InitializeUsers(YBUserContext context)
        {
            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            var user = new YBUser[]
            {
                new YBUser{Id=1, UserName = "fshen", FirstName="Frank",  FamilyName="Shen", UserRoles=("user")},
                new YBUser{Id=2, UserName = "admin", FirstName="Meredith",FamilyName="Alonso",    UserRoles=("user")},
                new YBUser{Id=3, UserName = "noman", FirstName="Arturo",  FamilyName="Anand",     UserRoles=("user")},
            };
            context.Users.AddRange(user);
            context.SaveChanges();
        }

        internal static void InitializeCars(YBCarContext context)
        {
            // Look for any students.
            if (context.Cars.Any())
            {
                return;   // DB has been seeded
            }
            var user = new YBCar[]
            {
                new YBCar{Id=1, Make = "Ford", Model="Mustung",  Year= 2000, Mileage=20090, DayRentPrice=100, IsAvailable=true, MaxRentPeriod=10, MinRentPeriod=1},
            };
            context.Cars.AddRange(user);
            context.SaveChanges();
        }

        internal static void InitializeOrders(YBRentContext context)
        {
            // Look for any students.
            if (context.Rents.Any())
            {
                return;   // DB has been seeded
            }
            var order = new YBRent[]
            {
                new YBRent{Id=1, CarId = 1, UserId=1, DateOfOrder=DateTime.Now, RentDays =3, RentStart=DateTime.Now, Status = YB_RentalStatus.pending.ToString() },
            };
            context.Rents.AddRange(order);
            context.SaveChanges();
        }
    }
}