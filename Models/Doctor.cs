namespace DoctorAppointmentSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty; // Online | Offline
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; } = null!;
        public string? Qualification { get; set; }
        public int ExperienceYears { get; set; }
        public decimal ConsultationFee { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
    }

}
