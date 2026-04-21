using DoctorAppointmentSystem.DTOs.Auth;
using DoctorAppointmentSystem.Helpers;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Repositories.Interfaces;
using DoctorAppointmentSystem.Services.Interfaces;

namespace DoctorAppointmentSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtHelper _jwt;
        public AuthService(IUserRepository userRepo, JwtHelper jwt)
        { _userRepo = userRepo; _jwt = jwt; }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepo.EmailExistsAsync(dto.Email))
                throw new Exception("Email already registered.");
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                Phone = dto.Phone
            };
            await _userRepo.CreateAsync(user);
            return _jwt.GenerateToken(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email)
                ?? throw new Exception("Invalid credentials.");
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials.");
            return _jwt.GenerateToken(user);
        }

        public async Task<UserProfileDto> GetProfileAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            return new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserProfileDto> UpdateProfileAsync(int userId, UserProfileDto dto)
        {
            var user = await _userRepo.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            user.FullName = dto.FullName; user.Phone = dto.Phone;
            await _userRepo.UpdateAsync(user);
            return dto;
        }
    }

}
