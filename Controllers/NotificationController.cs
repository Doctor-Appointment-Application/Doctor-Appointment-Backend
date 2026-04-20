using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentSystem.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
