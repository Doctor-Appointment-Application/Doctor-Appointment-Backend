namespace DoctorAppointmentSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Type { get; set; } = string.Empty;  // Confirmation | Reminder
        public string Message { get; set; } = string.Empty;
        public bool IsSent { get; set; } = false;
        public DateTime ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
