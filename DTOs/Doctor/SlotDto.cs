namespace DoctorAppointmentSystem.DTOs.Doctor
{
    public class SlotDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsBooked { get; set; }
    }

}
