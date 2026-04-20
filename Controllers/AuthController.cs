using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
