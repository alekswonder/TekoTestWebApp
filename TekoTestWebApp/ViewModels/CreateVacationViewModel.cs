using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.ViewModels
{
    public class CreateVacationViewModel
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        [Required(ErrorMessage = "The User field is required.")]
        public int User { get; set; }
    }
}
