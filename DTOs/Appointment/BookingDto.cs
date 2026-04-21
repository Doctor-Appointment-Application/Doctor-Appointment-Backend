using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentSystem.DTOs.Appointment
{
    public class BookingDto
    {
        [Required] public int DoctorId { get; set; }
        [Required] public int TimeSlotId { get; set; }
        public string? Notes { get; set; }
    }

}
