using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
