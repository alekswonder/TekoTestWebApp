using System.ComponentModel.DataAnnotations;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.ViewModels
{
    public class EditVacationViewModel
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        [Required(ErrorMessage = "The User field is required.")]
        public int User { get; set; }
    }
}
