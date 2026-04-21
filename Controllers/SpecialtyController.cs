using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using DoctorAppointmentSystem.DTOs.Doctor;
using DoctorAppointmentSystem.Services.Interfaces;

namespace DoctorAppointmentSystem.Controllers
{
    [ApiController, Route("api/specialties")]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyService _svc;
        public SpecialtyController(ISpecialtyService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _svc.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _svc.GetByIdAsync(id));

        [Authorize(Roles = "Admin"), HttpPost]
        public async Task<IActionResult> Create([FromBody] SpecialtyDto dto)
            => Ok(await _svc.CreateAsync(dto));

        [Authorize(Roles = "Admin"), HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecialtyDto dto)
            => Ok(await _svc.UpdateAsync(id, dto));

        [Authorize(Roles = "Admin"), HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }
    }
}
