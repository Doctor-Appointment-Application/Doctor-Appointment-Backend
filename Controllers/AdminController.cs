using DoctorAppointmentSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    [ApiController, Route("api/admin"), Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminSvc;
        public AdminController(IAdminService svc) => _adminSvc = svc;

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
            => Ok(await _adminSvc.GetDashboardAsync());

        [HttpGet("analytics")]
        public async Task<IActionResult> Analytics([FromQuery] DateTime from,
                                                   [FromQuery] DateTime to)
            => Ok(await _adminSvc.GetAnalyticsAsync(from, to));
    }
    

}
