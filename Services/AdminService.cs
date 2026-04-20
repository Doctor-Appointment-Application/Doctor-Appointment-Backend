using DoctorAppointmentSystem.DTOs.Admin;
using DoctorAppointmentSystem.Repositories.Interfaces;
using DoctorAppointmentSystem.Services.Interfaces;

namespace DoctorAppointmentSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;
        public AdminService(IAdminRepository repo) => _repo = repo;

        public async Task<DashboardDto> GetDashboardAsync()
            => await _repo.GetDashboardStatsAsync();

        public async Task<AnalyticsDto> GetAnalyticsAsync(DateTime from, DateTime to)
            => await _repo.GetAnalyticsAsync(from, to);
    }
}
