using DoctorAppointmentSystem.Data;
using DoctorAppointmentSystem.DTOs.Admin;
using DoctorAppointmentSystem.Models;
using DoctorAppointmentSystem.Repositories.Interfaces;

namespace DoctorAppointmentSystem.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context) => _context = context;

        public async Task<DashboardDto> GetDashboardStatsAsync()
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);
            var todayAppts = await _context.Appointments
                .Include(a => a.Doctor).ThenInclude(d => d.Specialty)
                .Include(a => a.TimeSlot)
                .Where(a => a.TimeSlot.StartTime >= today &&
                            a.TimeSlot.StartTime < tomorrow).ToListAsync();
            return new DashboardDto
            {
                TotalDoctors = await _context.Doctors.CountAsync(d => d.IsActive),
                TotalPatients = await _context.Users.CountAsync(u => u.Role == "Patient"),
                TodayAppointments = todayAppts.Count,
                OnlineAppointments = todayAppts.Count(a => a.Mode == "Online"),
                OfflineAppointments = todayAppts.Count(a => a.Mode == "Offline"),
                TodayRevenue = todayAppts.Sum(a => a.Doctor.ConsultationFee),
                SpecialtyBreakdown = todayAppts.GroupBy(a => a.Doctor.Specialty.Name)
                    .Select(g => new SpecialtyStatsDto
                    {
                        Specialty = g.Key,
                        Count = g.Count(),
                        Revenue = g.Sum(a => a.Doctor.ConsultationFee)
                    }).ToList()
            };
        }

        public async Task<AnalyticsDto> GetAnalyticsAsync(DateTime from, DateTime to)
        {
            var appts = await _context.Appointments
                .Include(a => a.Doctor).ThenInclude(d => d.Specialty)
                .Include(a => a.TimeSlot)
                .Where(a => a.TimeSlot.StartTime >= from &&
                            a.TimeSlot.StartTime < to).ToListAsync();
            return new AnalyticsDto
            {
                From = from,
                To = to,
                TotalRevenue = appts.Sum(a => a.Doctor.ConsultationFee),
                TotalAppointments = appts.Count,
                TopDoctors = appts.GroupBy(a => a.Doctor.FullName)
                    .ToDictionary(g => g.Key, g => g.Count()),
            };
        }

        public async Task<List<Doctor>> GetDoctorsAvailabilityAsync()
            => await _context.Doctors.Include(d => d.TimeSlots)
                 .Where(d => d.IsActive).ToListAsync();
    }

}
