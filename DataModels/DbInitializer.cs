using System.Diagnostics;
using YBCarRental3D_API.DataContexts;

namespace YBCarRental3D_API.DataModels
{
    public static class DbInitializer
    {
        public static void Initialize(YBUserContext context)
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
    }
}