using TekoTestWebApp.Interfaces;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.Services
{
    public class VacationService : IVacationService
    {
        private readonly IVacationRepository _vacationRepository;

        private bool IsDateRangeIntersect(Vacation vacationLeft, Vacation vacationRight)
        {
            return vacationLeft.StartDateTime <= vacationRight.EndDateTime
                && vacationRight.StartDateTime <= vacationLeft.EndDateTime;
        }

        public VacationService(IVacationRepository vacationRepository)
        { 
            _vacationRepository = vacationRepository;
        }

        public IEnumerable<Vacation> GetAge50PlusIntersections(Vacation vacation)
        {
            return _vacationRepository.GetAllAsync()
                .Result
                .Where(v => v.Id != vacation.Id
                && v.User.Age > 49
                && IsDateRangeIntersect(v, vacation))
                .ToList();
        }

        public IEnumerable<Vacation> GetDepartmentIntersections(Vacation vacation)
        {
            return _vacationRepository.GetAllAsync()
                .Result
                .Where(v => v.Id != vacation.Id 
                && v.User.Department == vacation.User.Department
                && IsDateRangeIntersect(v, vacation))
                .ToList();
        }

        public IEnumerable<Vacation> GetNonIntersectingVacations(Vacation vacation)
        {
            return _vacationRepository.GetAllAsync()
                .Result
                .Where(v => v.Id != vacation.Id
                && v.User.Id != vacation.User.Id
                && !IsDateRangeIntersect(v, vacation))
                .ToList ();
        }

        public IEnumerable<Vacation> GetWomenIntersections(Vacation vacation)
        {
            return _vacationRepository.GetAllAsync()
                .Result
                .Where(v => v.Id != vacation.Id
                && v.User.Department != vacation.User.Department
                && v.User.Gender == Data.Enum.Gender.Female
                && v.User.Age > 30
                && v.User.Age < 50
                && IsDateRangeIntersect(v, vacation))
                .ToList();
        }
    }
}
