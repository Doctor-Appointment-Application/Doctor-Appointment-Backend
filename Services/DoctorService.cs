using DoctorAppointmentSystem.DTOs.Doctor;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Repositories.Interfaces;
using DoctorAppointmentSystem.Services.Interfaces;

namespace DoctorAppointmentSystem.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo) => _repo = repo;

        public async Task<List<DoctorDto>> GetDoctorsAsync(string? mode, int? specialtyId)
        {
            var doctors = await _repo.GetAllAsync(mode, specialtyId);
            return doctors.Select(d => MapToDto(d)).ToList();
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var d = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Doctor not found.");
            return MapToDto(d);
        }

        public async Task<DoctorDto> CreateDoctorAsync(DoctorCreateDto dto)
        {
            var doctor = new Doctor
            {
                FullName = dto.FullName,
                Email = dto.FullName.ToLower().Replace(" ", ".") + "@clinic.com",
                Mode = dto.Mode,
                SpecialtyId = dto.SpecialtyId,
                Qualification = dto.Qualification,
                ExperienceYears = dto.ExperienceYears,
                ConsultationFee = dto.ConsultationFee
            };
            await _repo.CreateAsync(doctor);
            return await GetDoctorByIdAsync(doctor.Id);
        }

        public async Task<DoctorDto> UpdateDoctorAsync(int id, DoctorCreateDto dto)
        {
            var d = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Doctor not found.");
            d.FullName = dto.FullName; d.Mode = dto.Mode;
            d.SpecialtyId = dto.SpecialtyId; d.Qualification = dto.Qualification;
            d.ExperienceYears = dto.ExperienceYears; d.ConsultationFee = dto.ConsultationFee;
            await _repo.UpdateAsync(d);
            return MapToDto(d);
        }

        public async Task DeleteDoctorAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<List<SlotDto>> GetAvailableSlotsAsync(int doctorId)
        {
            var slots = await _repo.GetAvailableSlotsAsync(doctorId);
            return slots.Select(s => new SlotDto
            {
                Id = s.Id,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                IsBooked = s.IsBooked
            }).ToList();
        }

        public async Task<SlotDto> AddSlotAsync(int doctorId, SlotDto slotDto)
        {
            var slot = new TimeSlot
            {
                DoctorId = doctorId,
                StartTime = slotDto.StartTime,
                EndTime = slotDto.EndTime
            };
            await _repo.AddSlotAsync(slot);
            slotDto.Id = slot.Id;
            return slotDto;
        }

        private static DoctorDto MapToDto(Doctor d) => new DoctorDto
        {
            Id = d.Id,
            FullName = d.FullName,
            Mode = d.Mode,
            SpecialtyName = d.Specialty?.Name ?? "",
            Qualification = d.Qualification,
            ExperienceYears = d.ExperienceYears,
            ConsultationFee = d.ConsultationFee,
            AvailableSlots = d.TimeSlots?
                .Where(s => !s.IsBooked && !s.IsLocked && s.StartTime > DateTime.UtcNow)
                .Select(s => new SlotDto
                {
                    Id = s.Id,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    IsBooked = s.IsBooked
                }).ToList() ?? new()
        };
    }
}
