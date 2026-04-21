using DoctorAppointmentSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    [ApiController, Route("api/users"), Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userSvc;
        public UserController(IUserService userSvc) => _userSvc = userSvc;

        [Authorize(Roles = "Admin"), HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _userSvc.GetAllUsersAsync());

        [Authorize(Roles = "Admin"), HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _userSvc.GetUserByIdAsync(id));

        [Authorize(Roles = "Admin"), HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _userSvc.DeactivateUserAsync(id);
            return NoContent();
        }
    }
}
