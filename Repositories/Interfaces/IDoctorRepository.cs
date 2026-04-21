using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllAsync(string? mode, int? specialtyId);
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor> CreateAsync(Doctor doctor);
        Task<Doctor> UpdateAsync(Doctor doctor);
        Task DeleteAsync(int id);
        Task<List<TimeSlot>> GetAvailableSlotsAsync(int doctorId);
        Task<TimeSlot> AddSlotAsync(TimeSlot slot);
        Task<TimeSlot?> GetSlotByIdAsync(int slotId);
        Task LockSlotAsync(int slotId);
        Task UnlockSlotAsync(int slotId);
    }

}
