using DoctorAppointmentSystem.DTOs.Appointment;
using DoctorAppointmentSystem.Services.Interfaces;
using System.Net.Mail;

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
            message.Subject = "Appointment Confirmed";
            message.Body = new TextPart("html")
            {
                Text = $"<h2>Appointment Confirmed</h2>" +
                       $"<p>Doctor: {apt.DoctorName}</p>" +
                       $"<p>Mode: {apt.Mode}</p>" +
                       $"<p>Time: {apt.StartTime:f}</p>" +
                       (apt.Mode == "Online"
                         ? $"<p>Join Link: {apt.Artifact}</p>"
                         : $"<p>Clinic Address: {apt.Artifact}</p>")
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
            // Same as above but with Reminder subject/content
            // Called by a background service 24h before appointment
        }
    }

}
