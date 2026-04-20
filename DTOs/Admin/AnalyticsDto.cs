namespace DoctorAppointmentSystem.DTOs.Admin
{
    ppublic class AnalyticsDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<DailySummaryDto> DailyBreakdown { get; set; } = new();
        public decimal TotalRevenue { get; set; }
        public int TotalAppointments { get; set; }
        public Dictionary<string, int> TopDoctors { get; set; } = new();
    }

}
