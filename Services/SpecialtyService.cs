using DoctorAppointmentSystem.DTOs.Doctor;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Repositories.Interfaces;
using DoctorAppointmentSystem.Services.Interfaces;

namespace DoctorAppointmentSystem.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _repo;
        public SpecialtyService(ISpecialtyRepository repo) => _repo = repo;

        public async Task<List<SpecialtyDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(s => new SpecialtyDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            }).ToList();
        }

        public async Task<SpecialtyDto> GetByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Specialty not found.");
            return new SpecialtyDto { Id = s.Id, Name = s.Name, Description = s.Description };
        }

        public async Task<SpecialtyDto> CreateAsync(SpecialtyDto dto)
        {
            var s = new Specialty { Name = dto.Name, Description = dto.Description };
            await _repo.CreateAsync(s);
            dto.Id = s.Id;
            return dto;
        }

        public async Task<SpecialtyDto> UpdateAsync(int id, SpecialtyDto dto)
        {
            var s = await _repo.GetByIdAsync(id)
                ?? throw new Exception("Specialty not found.");
            s.Name = dto.Name; s.Description = dto.Description;
            await _repo.UpdateAsync(s);
            return dto;
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
