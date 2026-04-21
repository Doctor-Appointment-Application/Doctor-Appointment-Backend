using DoctorAppointmentSystem.DTOs.Appointment;

namespace DoctorAppointmentSystem.DTOs.Admin
{
    public class AnalyticsDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int TotalAppointments { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OnlineCount { get; set; }
        public int OfflineCount { get; set; }
        public List<SpecialtyBreakdownDto> BySpecialty { get; set; } = new();

        // These two were missing ↓
        public Dictionary<string, int> TopDoctors { get; set; } = new();
        public List<DailySummaryDto> DailyBreakdown { get; set; } = new();
    }
}