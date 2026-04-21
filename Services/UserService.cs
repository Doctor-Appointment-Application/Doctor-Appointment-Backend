using DoctorAppointmentSystem.DTOs.Auth;
using DoctorAppointmentSystem.Repositories.Interfaces;
using DoctorAppointmentSystem.Services.Interfaces;

namespace DoctorAppointmentSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<List<UserProfileDto>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllAsync();
            return users.Select(u => new UserProfileDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList();
        }

        public async Task<UserProfileDto> GetUserByIdAsync(int id)
        {
            var u = await _repo.GetByIdAsync(id)
                ?? throw new Exception("User not found.");
            return new UserProfileDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            };
        }

        public async Task<bool> DeactivateUserAsync(int id)
        {
            var u = await _repo.GetByIdAsync(id)
                ?? throw new Exception("User not found.");
            u.IsActive = false;
            await _repo.UpdateAsync(u);
            return true;
        }
    }
}
