using Microsoft.AspNetCore.Identity;
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
                }

                for (int i = 2; i < 101; i++)
                {

                }
            }
        }
    }
}
