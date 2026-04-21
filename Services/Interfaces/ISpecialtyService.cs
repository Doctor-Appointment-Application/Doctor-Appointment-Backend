using DoctorAppointmentSystem.DTOs.Doctor;

namespace DoctorAppointmentSystem.Services.Interfaces
{
    public interface ISpecialtyService
    {
        Task<List<SpecialtyDto>> GetAllAsync();
        Task<SpecialtyDto> GetByIdAsync(int id);
        Task<SpecialtyDto> CreateAsync(SpecialtyDto dto);
        Task<SpecialtyDto> UpdateAsync(int id, SpecialtyDto dto);
        Task DeleteAsync(int id);
    }
}