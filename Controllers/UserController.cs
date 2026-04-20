using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
