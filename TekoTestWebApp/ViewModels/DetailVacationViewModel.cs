using TekoTestWebApp.Models;

namespace TekoTestWebApp.ViewModels
{
    public class DetailVacationViewModel
    {
        public Vacation Vacation { get; set; }
        public IEnumerable<Vacation> DepartmentIntersections { get; set; }
        public IEnumerable<Vacation> WomenIntersections { get; set; }
        public IEnumerable<Vacation> Age50PlusIntersections { get; set; }
        public IEnumerable<Vacation> NonIntersectingVacations { get; set; }
    }
}
