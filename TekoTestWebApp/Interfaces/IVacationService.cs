using TekoTestWebApp.Models;

namespace TekoTestWebApp.Interfaces
{
    public interface IVacationService
    {
        IEnumerable<Vacation> GetDepartmentIntersections(Vacation vacation);
        IEnumerable<Vacation> GetWomenIntersections(Vacation vacation);
        IEnumerable<Vacation> GetAge50PlusIntersections(Vacation vacation);
        IEnumerable<Vacation> GetNonIntersectingVacations(Vacation vacation);
    }
}
