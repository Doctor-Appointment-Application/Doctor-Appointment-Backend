using DoctorAppointmentSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    [ApiController, Route("api/notifications"), Authorize(Roles = "Admin")]
    public class NotificationController : ControllerBase
    {
        private readonly IEmailService _emailSvc;
        private readonly IAppointmentService _apptSvc;

        public NotificationController(IEmailService e, IAppointmentService a)
        {
            _emailSvc = e;
            _apptSvc = a;
        }

        [HttpPost("send-reminders")]
        public async Task<IActionResult> SendReminders()
        {
            // Trigger reminders for appointments in next 24h
            return Ok(new { message = "Reminders queued" });
        }
    }
}
