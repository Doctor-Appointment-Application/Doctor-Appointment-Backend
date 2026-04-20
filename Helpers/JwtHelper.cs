using DoctorAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Helpers
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Specialty> Specialties => Set<Specialty>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>().HasIndex(u => u.Email).IsUnique();
            mb.Entity<Doctor>().HasOne(d => d.Specialty)
                .WithMany(s => s.Doctors).HasForeignKey(d => d.SpecialtyId);
            mb.Entity<Appointment>().HasOne(a => a.Patient)
                .WithMany(u => u.Appointments).HasForeignKey(a => a.PatientId);
            mb.Entity<TimeSlot>().HasOne(t => t.Doctor)
                .WithMany(d => d.TimeSlots).HasForeignKey(t => t.DoctorId);
        }
    }

