using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
