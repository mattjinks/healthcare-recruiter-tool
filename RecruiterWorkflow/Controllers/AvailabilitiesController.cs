using Microsoft.AspNetCore.Mvc;

namespace RecruiterWorkflow.Controllers
{
    public class AvailabilitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
