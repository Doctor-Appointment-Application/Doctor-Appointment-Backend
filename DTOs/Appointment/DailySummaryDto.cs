namespace DoctorAppointmentSystem.DTOs.Appointment
{
    public class DailySummaryDto
    {
        public DateTime Date { get; set; }
        public int TotalAppointments { get; set; }
        public int OnlineCount { get; set; }
        public int OfflineCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public Dictionary<string, int> BySpecialty { get; set; } = new();
    }

}
