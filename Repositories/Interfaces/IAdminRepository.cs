using DoctorAppointmentSystem.DTOs.Admin;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<DashboardDto> GetDashboardStatsAsync();
        Task<AnalyticsDto> GetAnalyticsAsync(DateTime from, DateTime to);
        Task<List<Doctor>> GetDoctorsAvailabilityAsync();
    }

}
