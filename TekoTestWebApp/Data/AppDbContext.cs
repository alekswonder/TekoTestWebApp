using Microsoft.EntityFrameworkCore;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }
        public DbSet<Vacation> Vacation { get; set; }
        public DbSet<User> User { get; set; }
    }
}
