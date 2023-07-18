using TekoTestWebApp.Models;

namespace TekoTestWebApp.ViewModels
{
    public class CreateVacationViewModel
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public User User { get; set; }
    }
}
