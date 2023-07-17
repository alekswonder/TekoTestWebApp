using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TekoTestWebApp.Data.Enum;

namespace TekoTestWebApp.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public Position Position { get; set; }
        public Department Department { get; set; }
        public int Age { get; set; }
        public DateTime? RegistrationTime { get; set; }
    }
}
