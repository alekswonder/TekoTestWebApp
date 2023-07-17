using TekoTestWebApp.Data.Enum;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if (!context.Users.Any())
                {
                    Random random = new Random();
                    Type typeOfPosition = typeof(Position);
                    Type typeOfDepartment = typeof(Department);
                    Array positions = typeOfPosition.GetEnumValues();
                    Array departments = typeOfDepartment.GetEnumValues() ;
                    for (int i = 1; i < 101; i++)
                    {
                        new User()
                        {
                            FullName = string.Format("Employee №{0}", i),
                            Position = (Position)positions.GetValue(random.Next(positions.Length)),
                            Department = (Department)departments.GetValue(random.Next(departments.Length)),
                            Age = random.Next(24, 55)
                        };
                    }
                }
            }
        }
    }
}
