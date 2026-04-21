using Microsoft.EntityFrameworkCore;
using DoctorAppointmentSystem.Data;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Repositories.Interfaces;

namespace DoctorAppointmentSystem.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly AppDbContext _context;
        public SpecialtyRepository(AppDbContext context) => _context = context;

        public async Task<List<Specialty>> GetAllAsync()
            => await _context.Specialties.Include(s => s.Doctors).ToListAsync();

        public async Task<Specialty?> GetByIdAsync(int id)
            => await _context.Specialties.Include(s => s.Doctors)
                 .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Specialty> CreateAsync(Specialty specialty)
        {
            _context.Specialties.Add(specialty);
            await _context.SaveChangesAsync();
            return specialty;
        }

        public async Task<Specialty> UpdateAsync(Specialty specialty)
        {
            _context.Specialties.Update(specialty);
            await _context.SaveChangesAsync();
            return specialty;
        }

        public async Task DeleteAsync(int id)
        {
            var s = await _context.Specialties.FindAsync(id);
            if (s != null) { _context.Specialties.Remove(s); await _context.SaveChangesAsync(); }
        }
    }
}
