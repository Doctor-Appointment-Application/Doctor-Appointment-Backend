namespace DoctorAppointmentSystem.DTOs.Appointment
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string SpecialtyName { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Artifact { get; set; }
        public DateTime BookedAt { get; set; }
    }

}
