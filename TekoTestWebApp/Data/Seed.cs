using Microsoft.AspNetCore.Identity;
using TekoTestWebApp.Data.Enum;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.Data
{
    public class Seed
    {
        private static void AddVacationToUser(User user, Random random, IApplicationBuilder appBuilder)
        {
            using (var serviceInnerScope = appBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceInnerScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                List<Vacation> vacations = new List<Vacation>();
                List<double> days = new List<double>() { 14d, 7d, 7d };
                days = days.OrderBy(d => random.Next()).ToList();
                DateTime starDate = DateTime.Now.AddDays((double)random.Next(5, 15));
                for (int i = 0; i < 3; i++)
                {
                    var start = starDate;
                    var end = start.AddDays(days[i]);
                    vacations.Add(new Vacation()
                    {
                        StartDateTime = start,
                        EndDateTime = end,
                        User = user
                    });
                    starDate = end;
                    starDate = starDate.AddDays((double)random.Next(90, 109));
                }
                context.Vacations.AddRange(vacations);
                context.SaveChanges();
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                // Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                // Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "adminUserEmail@somedomain.com";
                Random random = new Random();
                Type typeOfDepartment = typeof(Department);
                Type typeOfGender = typeof(Gender);
                Type typeOfPosition = typeof(Position);
                Array departments = typeOfDepartment.GetEnumValues();
                Array genders = typeOfGender.GetEnumValues();
                Array positions = typeOfPosition.GetEnumValues();
                


                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        FullName = "Nepein Alexander Askhtovich",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Position = Position.SeniorBackend,
                        Department = Department.WebDevelopment,
                        Age = 24,
                        RegistrationTime = DateTime.Now,
                        Gender = Gender.Male
                    };
                    await userManager.CreateAsync(newAdminUser, "admin");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                    AddVacationToUser(newAdminUser, random, appBuilder);
                }

                for (int i = 2; i < 101; i++)
                {
                    string userEmail = string.Format("employee№{0}@somedomain.com", i);
                    var appUser = await userManager.FindByEmailAsync(userEmail); 
                    if (appUser == null)
                    {
                        Gender genderValue = (Gender)genders.GetValue(random.Next(genders.Length));
                        string empGender = genderValue == Gender.Male ? "Male" : "Female";
                        var newUser = new User()
                        {
                            FullName = string.Format("Unnamed {0} with ID#{1}", empGender, i),
                            Email = userEmail,
                            EmailConfirmed = true,
                            Position = (Position)positions.GetValue(random.Next(positions.Length)),
                            Department = (Department)departments.GetValue(random.Next(departments.Length)),
                            Age = random.Next(18, 56),
                            RegistrationTime = DateTime.Now,
                            Gender = genderValue
                        };
                        await userManager.CreateAsync(newUser, "emp");
                        await userManager.AddToRoleAsync(newUser, UserRoles.User);
                        AddVacationToUser(newUser, random, appBuilder);
                    }
                }
            }
        }
    }
}
