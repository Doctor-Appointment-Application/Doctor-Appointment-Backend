using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentSystem.DTOs.Doctor
{
    public class DoctorCreateDto
    {
        [Required] public string FullName { get; set; } = string.Empty;
        [Required] public string Mode { get; set; } = string.Empty;
        [Required] public int SpecialtyId { get; set; }
        public string? Qualification { get; set; }
        public int ExperienceYears { get; set; }
        public decimal ConsultationFee { get; set; }
    }


}
