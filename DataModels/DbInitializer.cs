using System.Diagnostics;
using YBCarRental3D_API.DataContexts;

namespace YBCarRental3D_API.DataModels
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