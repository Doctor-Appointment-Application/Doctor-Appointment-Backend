using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
