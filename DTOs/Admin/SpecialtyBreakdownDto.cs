namespace DoctorAppointmentSystem.DTOs.Admin
{
    public class SpecialtyBreakdownDto
    {
        public string Specialty { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal Revenue { get; set; }
    }
}