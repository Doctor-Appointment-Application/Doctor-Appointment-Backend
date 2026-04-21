using DoctorAppointmentSystem.DTOs.Appointment;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Repositories.Interfaces;
using DoctorAppointmentSystem.Services.Interfaces;

namespace DoctorAppointmentSystem.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepo;

        public AppointmentService(
            IAppointmentRepository repo,
            IDoctorRepository doctorRepo,
            IEmailService emailService,
            IUserRepository userRepo)
        {
            _repo = repo;
            _doctorRepo = doctorRepo;
            _emailService = emailService;
            _userRepo = userRepo;
        }

        public async Task<AppointmentResponseDto> BookAppointmentAsync(int patientId, BookingDto dto)
        {
            // 1. Get slot and lock it (prevent double booking)
            var slot = await _doctorRepo.GetSlotByIdAsync(dto.TimeSlotId)
                ?? throw new Exception("Slot not found.");
            if (slot.IsBooked || slot.IsLocked)
                throw new Exception("Slot is no longer available.");

            await _doctorRepo.LockSlotAsync(dto.TimeSlotId);

            try
            {
                // 2. Get doctor to determine mode & artifact
                var doctor = await _doctorRepo.GetByIdAsync(dto.DoctorId)
                    ?? throw new Exception("Doctor not found.");

                string artifact = doctor.Mode == "Online"
                    ? $"https://meet.doctorapp.com/room/{Guid.NewGuid():N}"
                    : $"Clinic: {doctor.FullName}'s Office, Ground Floor, City Medical Center";

                // 3. Create appointment
                var appointment = new Appointment
                {
                    PatientId = patientId,
                    DoctorId = dto.DoctorId,
                    TimeSlotId = dto.TimeSlotId,
                    Mode = doctor.Mode,
                    Status = "Confirmed",
                    Artifact = artifact,
                    Notes = dto.Notes
                };

                await _repo.BookAsync(appointment);
                slot.IsBooked = true;
                await _doctorRepo.UnlockSlotAsync(dto.TimeSlotId);

                // 4. Build response
                var response = await MapToResponseDto(appointment);

                // 5. Send confirmation email (non-blocking)
                var patient = await _userRepo.GetByIdAsync(patientId);
                if (patient != null)
                    _ = _emailService.SendConfirmationAsync(patient.Email, response);

                return response;
            }
            catch
            {
                // Rollback lock on failure
                await _doctorRepo.UnlockSlotAsync(dto.TimeSlotId);
                throw;
            }
        }

        public async Task<List<AppointmentResponseDto>> GetMyAppointmentsAsync(int patientId)
        {
            var appointments = await _repo.GetByPatientAsync(patientId);
            var result = new List<AppointmentResponseDto>();
            foreach (var a in appointments)
                result.Add(await MapToResponseDto(a));
            return result;
        }

        public async Task<AppointmentResponseDto> UpdateStatusAsync(int appointmentId, string status)
        {
            var validStatuses = new[] { "Confirmed", "Completed", "Cancelled", "NoShow" };
            if (!validStatuses.Contains(status))
                throw new Exception($"Invalid status. Use: {string.Join(", ", validStatuses)}");

            var appt = await _repo.UpdateStatusAsync(appointmentId, status);
            return await MapToResponseDto(appt);
        }

        public async Task<DailySummaryDto> GetDailySummaryAsync(DateTime date, string? mode, string? specialty)
            => await _repo.GetDailySummaryAsync(date, mode, specialty);

        private async Task<AppointmentResponseDto> MapToResponseDto(Appointment a)
        {
            // Re-fetch if navigation props missing
            if (a.Doctor == null)
                a = await _repo.GetByIdAsync(a.Id) ?? a;

            return new AppointmentResponseDto
            {
                Id = a.Id,
                PatientName = a.Patient?.FullName ?? "",
                DoctorName = a.Doctor?.FullName ?? "",
                SpecialtyName = a.Doctor?.Specialty?.Name ?? "",
                Mode = a.Mode,
                StartTime = a.TimeSlot?.StartTime ?? DateTime.MinValue,
                EndTime = a.TimeSlot?.EndTime ?? DateTime.MinValue,
                Status = a.Status,
                Artifact = a.Artifact,
                BookedAt = a.BookedAt
            };
        }
    }

}
