using DoctorAppointmentSystem.DTOs.Appointment;
using DoctorAppointmentSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoctorAppointmentSystem.Controllers
{
    [ApiController, Route("api/appointments"), Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _svc;
        public AppointmentController(IAppointmentService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Book([FromBody] BookingDto dto)
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _svc.BookAppointmentAsync(patientId, dto));
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyAppointments()
        {
            var patientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _svc.GetMyAppointmentsAsync(patientId));
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
            => Ok(await _svc.UpdateStatusAsync(id, status));

        [HttpGet("summary")]
        public async Task<IActionResult> DailySummary([FromQuery] DateTime date,
            [FromQuery] string? mode, [FromQuery] string? specialty)
            => Ok(await _svc.GetDailySummaryAsync(date, mode, specialty));
    }

}
