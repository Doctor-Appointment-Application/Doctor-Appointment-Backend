using DoctorAppointmentSystem.DTOs.Appointment;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Repositories.Interfaces;
using DoctorAppointmentSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;
        public AppointmentRepository(AppDbContext context) => _context = context;

        public async Task<Appointment> BookAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<Appointment>> GetByPatientAsync(int patientId)
            => await _context.Appointments.Include(a => a.Doctor).ThenInclude(d => d.Specialty)
                 .Include(a => a.TimeSlot).Where(a => a.PatientId == patientId)
                 .OrderByDescending(a => a.BookedAt).ToListAsync();

        public async Task<List<Appointment>> GetByDoctorAsync(int doctorId)
            => await _context.Appointments.Include(a => a.Patient)
                 .Include(a => a.TimeSlot).Where(a => a.DoctorId == doctorId)
                 .OrderByDescending(a => a.BookedAt).ToListAsync();

        public async Task<Appointment?> GetByIdAsync(int id)
            => await _context.Appointments.Include(a => a.Doctor)
                 .ThenInclude(d => d.Specialty).Include(a => a.Patient)
                 .Include(a => a.TimeSlot).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Appointment> UpdateStatusAsync(int id, string status)
        {
            var appt = await _context.Appointments.FindAsync(id)
                ?? throw new Exception("Appointment not found");
            appt.Status = status;
            await _context.SaveChangesAsync();
            return appt;
        }

        public async Task<DailySummaryDto> GetDailySummaryAsync(DateTime date, string? mode, string? specialty)
        {
            var start = date.Date; var end = start.AddDays(1);
            var query = _context.Appointments.Include(a => a.Doctor)
                .ThenInclude(d => d.Specialty).Include(a => a.TimeSlot)
                .Where(a => a.TimeSlot.StartTime >= start && a.TimeSlot.StartTime < end);

            if (!string.IsNullOrEmpty(mode)) query = query.Where(a => a.Mode == mode);
            if (!string.IsNullOrEmpty(specialty))
                query = query.Where(a => a.Doctor.Specialty.Name == specialty);

            var list = await query.ToListAsync();
            return new DailySummaryDto
            {
                Date = date.Date,
                TotalAppointments = list.Count,
                OnlineCount = list.Count(a => a.Mode == "Online"),
                OfflineCount = list.Count(a => a.Mode == "Offline"),
                TotalRevenue = list.Sum(a => a.Doctor.ConsultationFee),
                BySpecialty = list.GroupBy(a => a.Doctor.Specialty.Name)
                                  .ToDictionary(g => g.Key, g => g.Count())
            };
        }

        public async Task<List<Appointment>> GetUpcomingAsync(DateTime from)
            => await _context.Appointments.Include(a => a.Patient)
                 .Include(a => a.TimeSlot).Where(a => a.TimeSlot.StartTime > from
                 && a.Status == "Confirmed").ToListAsync();
    }
}
