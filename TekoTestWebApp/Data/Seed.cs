using TekoTestWebApp.Data.Enum;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.Data
{
    public class Seed
    {
        private static List<Vacation> AddVacationToUser(User user, Random random)
        {
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
                return vacations;
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                Random random = new Random();
                Type typeOfDepartment = typeof(Department);
                Type typeOfGender = typeof(Gender);
                Type typeOfPosition = typeof(Position);
                Array departments = typeOfDepartment.GetEnumValues();
                Array genders = typeOfGender.GetEnumValues();
                Array positions = typeOfPosition.GetEnumValues();

                var newAdminUser = new User()
                {
                    Username = "admin",
                    Password = "1",
                    FullName = "Nepein Alexander Askhtovich",
                    Position = Position.JuniorBackend,
                    Department = Department.WebDevelopment,
                    Age = 24,
                    RegistrationTime = DateTime.Now,
                    Gender = Gender.Male
                };
                context.Vacation.AddRange(AddVacationToUser(newAdminUser, random));
                context.SaveChanges();

                for (int i = 2; i < 101; i++)
                { 
                    Gender genderValue = (Gender)genders.GetValue(random.Next(genders.Length));
                    string empGender = genderValue == Gender.Male ? "Male" : "Female";
                    var newUser = new User()
                    {
                        Username = string.Format("employee{0}", i),
                        Password = "1",
                        FullName = string.Format("Unnamed {0} with ID#{1}", empGender, i),
                        Position = (Position)positions.GetValue(random.Next(positions.Length)),
                        Department = (Department)departments.GetValue(random.Next(departments.Length)),
                        Age = random.Next(21, 56),
                        RegistrationTime = DateTime.Now,
                        Gender = genderValue
                    };
                    context.Vacation.AddRange(AddVacationToUser(newUser, random));
                    context.SaveChanges();
                }
            }
        }
    }
}
