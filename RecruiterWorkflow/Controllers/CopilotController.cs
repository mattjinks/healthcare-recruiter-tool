using Microsoft.AspNetCore.Mvc;
using RecruiterWorkflow.Services;

namespace RecruiterWorkflow.Controllers
{
    public class CopilotController : Controller
    {
        private readonly AIService _aiService;
        private readonly ILogger<HomeController> _logger;
        public CopilotController(AIService aiService, ILogger<HomeController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _aiService.RunExample();
            _logger.LogInformation("Index Action in Copilot Controller was called");
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
