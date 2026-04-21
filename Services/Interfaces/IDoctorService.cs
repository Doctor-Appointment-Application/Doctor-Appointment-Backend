using DoctorAppointmentSystem.DTOs.Doctor;

namespace DoctorAppointmentSystem.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetDoctorsAsync(string? mode, int? specialtyId);
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task<DoctorDto> CreateDoctorAsync(DoctorCreateDto dto);
        Task<DoctorDto> UpdateDoctorAsync(int id, DoctorCreateDto dto);
        Task DeleteDoctorAsync(int id);
        Task<List<SlotDto>> GetAvailableSlotsAsync(int doctorId);
        Task<SlotDto> AddSlotAsync(int doctorId, SlotDto slotDto);
    }

}
