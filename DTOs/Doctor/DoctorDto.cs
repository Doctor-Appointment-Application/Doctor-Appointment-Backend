namespace DoctorAppointmentSystem.DTOs.Doctor
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public string SpecialtyName { get; set; } = string.Empty;
        public string? Qualification { get; set; }
        public int ExperienceYears { get; set; }
        public decimal ConsultationFee { get; set; }
        public List<SlotDto> AvailableSlots { get; set; } = new();
    }

}
