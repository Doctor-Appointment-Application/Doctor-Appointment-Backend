using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Repositories.Interfaces;
using DoctorAppointmentSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context) => _context = context;

        public async Task<List<Doctor>> GetAllAsync(string? mode, int? specialtyId)
        {
            var query = _context.Doctors.Include(d => d.Specialty)
                         .Include(d => d.TimeSlots).Where(d => d.IsActive);
            if (!string.IsNullOrEmpty(mode)) query = query.Where(d => d.Mode == mode);
            if (specialtyId.HasValue) query = query.Where(d => d.SpecialtyId == specialtyId);
            return await query.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
            => await _context.Doctors.Include(d => d.Specialty)
                 .Include(d => d.TimeSlots).FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Doctor> CreateAsync(Doctor doctor)
        { _context.Doctors.Add(doctor); await _context.SaveChangesAsync(); return doctor; }

        public async Task<Doctor> UpdateAsync(Doctor doctor)
        { _context.Doctors.Update(doctor); await _context.SaveChangesAsync(); return doctor; }

        public async Task DeleteAsync(int id)
        {
            var doc = await _context.Doctors.FindAsync(id);
            if (doc != null) { doc.IsActive = false; await _context.SaveChangesAsync(); }
        }

        public async Task<List<TimeSlot>> GetAvailableSlotsAsync(int doctorId)
            => await _context.TimeSlots.Where(s => s.DoctorId == doctorId
                 && !s.IsBooked && !s.IsLocked && s.StartTime > DateTime.UtcNow).ToListAsync();

        public async Task<TimeSlot> AddSlotAsync(TimeSlot slot)
        { _context.TimeSlots.Add(slot); await _context.SaveChangesAsync(); return slot; }

        public async Task<TimeSlot?> GetSlotByIdAsync(int slotId)
            => await _context.TimeSlots.FindAsync(slotId);

        public async Task LockSlotAsync(int slotId)
        {
            var slot = await _context.TimeSlots.FindAsync(slotId);
            if (slot != null) { slot.IsLocked = true; await _context.SaveChangesAsync(); }
        }

        public async Task UnlockSlotAsync(int slotId)
        {
            var slot = await _context.TimeSlots.FindAsync(slotId);
            if (slot != null) { slot.IsLocked = false; await _context.SaveChangesAsync(); }
        }
    }

}
