using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TekoTestWebApp.Data.Enum;

namespace TekoTestWebApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public Position Position { get; set; }
        [ForeignKey("Department")]
        public int DeparmentId { get; set; }
        public Department Department { get; set; }
        public int Age { get; set; }
    }
}
