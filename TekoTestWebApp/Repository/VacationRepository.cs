using Microsoft.EntityFrameworkCore;
using TekoTestWebApp.Data;
using TekoTestWebApp.Interfaces;
using TekoTestWebApp.Models;

namespace TekoTestWebApp.Repository
{
    public class VacationRepository : IVacationRepository
    {
        private readonly AppDbContext _context;

        public VacationRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Vacation vacation)
        {
            _context.Add(vacation);
            return Save();
        }

        public bool Delete(Vacation vacation)
        {
            _context.Remove(vacation);
            return Save();
        }

        public async Task<IEnumerable<Vacation>> GetAllAsync()
        {
            return await _context.Vacation.ToListAsync();
        }

        public async Task<IEnumerable<Vacation>> GetByFullNameAsync(string fullname)
        {
            return await _context.Vacation.Where(v => v.User.FullName.Contains(fullname)).ToListAsync();
        }

        public async Task<Vacation> GetByIdAsync(int id)
        {
            return await _context.Vacation.Include(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Vacation> GetByIdNoTracking(int id)
        {
            return await _context.Vacation.Include(v => v.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vacation>> GetByUsernameAsync(string username)
        {
            return await _context.Vacation.Where(v => v.User.Username.Contains(username)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Vacation vacation)
        {
            _context.Update(vacation);
            return Save();
        }
    }
}
