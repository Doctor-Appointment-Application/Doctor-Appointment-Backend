using DoctorAppointmentSystem.DTOs.Appointment;

namespace DoctorAppointmentSystem.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseDto> BookAppointmentAsync(int patientId, BookingDto dto);
        Task<List<AppointmentResponseDto>> GetMyAppointmentsAsync(int patientId);
        Task<AppointmentResponseDto> UpdateStatusAsync(int appointmentId, string status);
        Task<DailySummaryDto> GetDailySummaryAsync(DateTime date, string? mode, string? specialty);
    }

}
