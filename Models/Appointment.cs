namespace DoctorAppointmentSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public User Patient { get; set; } = null!;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; } = null!;
        public string Mode { get; set; } = string.Empty; // Online | Offline
        public string Status { get; set; } = "Confirmed";
        // Status: Confirmed | Completed | Cancelled | NoShow
        public string? Artifact { get; set; }
        // Online: video link | Offline: clinic address
        public DateTime BookedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }

}
