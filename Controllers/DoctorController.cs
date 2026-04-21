using DoctorAppointmentSystem.DTOs.Doctor;
using DoctorAppointmentSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    [ApiController, Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService s) => _doctorService = s;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? mode,
                                                 [FromQuery] int? specialtyId)
            => Ok(await _doctorService.GetDoctorsAsync(mode, specialtyId));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _doctorService.GetDoctorByIdAsync(id));

        [Authorize(Roles = "Admin"), HttpPost]
        public async Task<IActionResult> Create([FromBody] DoctorCreateDto dto)
            => Ok(await _doctorService.CreateDoctorAsync(dto));

        [Authorize(Roles = "Admin"), HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorCreateDto dto)
            => Ok(await _doctorService.UpdateDoctorAsync(id, dto));

        [Authorize(Roles = "Admin"), HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        { await _doctorService.DeleteDoctorAsync(id); return NoContent(); }

        [HttpGet("{id}/slots")]
        public async Task<IActionResult> GetSlots(int id)
            => Ok(await _doctorService.GetAvailableSlotsAsync(id));

        [Authorize(Roles = "Admin,Doctor"), HttpPost("{id}/slots")]
        public async Task<IActionResult> AddSlot(int id, [FromBody] SlotDto dto)
            => Ok(await _doctorService.AddSlotAsync(id, dto));
    }

}
