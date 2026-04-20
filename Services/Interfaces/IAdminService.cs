using DoctorAppointmentSystem.DTOs.Admin;

namespace DoctorAppointmentSystem.Services.Interfaces
{
    public interface IAdminService
    {
        Task<DashboardDto> GetDashboardAsync();
        Task<AnalyticsDto> GetAnalyticsAsync(DateTime from, DateTime to);
    }

}
