using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Repositories.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<List<Specialty>> GetAllAsync();
        Task<Specialty?> GetByIdAsync(int id);
        Task<Specialty> CreateAsync(Specialty specialty);
        Task<Specialty> UpdateAsync(Specialty specialty);
        Task DeleteAsync(int id);
    }
}
