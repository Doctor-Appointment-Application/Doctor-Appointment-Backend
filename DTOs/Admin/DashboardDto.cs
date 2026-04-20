namespace DoctorAppointmentSystem.DTOs.Admin
{
    public class DashboardDto
    {
        public int TotalDoctors { get; set; }
        public int TotalPatients { get; set; }
        public int TodayAppointments { get; set; }
        public int OnlineAppointments { get; set; }
        public int OfflineAppointments { get; set; }
        public decimal TodayRevenue { get; set; }
        public List<SpecialtyStatsDto> SpecialtyBreakdown { get; set; } = new();
    }

}
