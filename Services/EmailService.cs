using DoctorAppointmentSystem.DTOs.Appointment;
using DoctorAppointmentSystem.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace DoctorAppointmentSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config) => _config = config;

        public async Task SendConfirmationAsync(string email, AppointmentResponseDto apt)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DoctorApp", _config["Email:From"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Appointment Confirmation";
            // SendConfirmationAsync
            message.Body = new TextPart("plain")
            {
                Text = $"Your appointment is confirmed.\nMode: {apt.Mode}\nDate: {apt.StartTime}"  // ← SlotTime → StartTime
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:Host"],
                int.Parse(_config["Email:Port"]!), false);
            await smtp.AuthenticateAsync(_config["Email:User"], _config["Email:Pass"]);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendReminderAsync(string email, AppointmentResponseDto apt)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("DoctorApp", _config["Email:From"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Appointment Reminder";
            

            // SendReminderAsync
            message.Body = new TextPart("plain")
            {
                Text = $"Reminder: You have an appointment tomorrow.\nMode: {apt.Mode}\nDate: {apt.StartTime}"  // ← SlotTime → StartTime
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:Host"],
                int.Parse(_config["Email:Port"]!), false);
            await smtp.AuthenticateAsync(_config["Email:User"], _config["Email:Pass"]);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}