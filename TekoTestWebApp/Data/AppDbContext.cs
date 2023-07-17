using Microsoft.EntityFrameworkCore;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Clubs { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
    }
}
