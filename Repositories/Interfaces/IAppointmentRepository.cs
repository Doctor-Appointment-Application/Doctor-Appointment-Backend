using DoctorAppointmentSystem.DTOs.Appointment;
using DoctorAppointmentSystem.Models;

namespace DoctorAppointmentSystem.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> BookAsync(Appointment appointment);
        Task<List<Appointment>> GetByPatientAsync(int patientId);
        Task<List<Appointment>> GetByDoctorAsync(int doctorId);
        Task<Appointment?> GetByIdAsync(int id);
        Task<Appointment> UpdateStatusAsync(int id, string status);
        Task<DailySummaryDto> GetDailySummaryAsync(DateTime date, string? mode, string? specialty);
        Task<List<Appointment>> GetUpcomingAsync(DateTime from);
    }

}
