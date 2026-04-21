using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentSystem.DTOs.Doctor
{
    public class SpecialtyDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

}
