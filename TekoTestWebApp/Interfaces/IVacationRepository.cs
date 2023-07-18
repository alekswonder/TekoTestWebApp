using TekoTestWebApp.Models;

namespace TekoTestWebApp.Interfaces
{
    public interface IVacationRepository
    {
        Task<IEnumerable<Vacation>> GetAllAsync();
        Task<Vacation> GetByIdAsync(int id);
        Task<IEnumerable<Vacation>> GetByUsernameAsync(string username);
        Task<IEnumerable<Vacation>> GetByFullNameAsync(string fullname);
        Task<Vacation> GetByIdNoTracking(int id);
        bool Add(Vacation vacation);
        bool Update(Vacation vacation);
        bool Delete(Vacation vacation);
        bool Save();
    }
}
