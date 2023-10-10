using Microsoft.AspNetCore.Mvc;

namespace SurveyMVC.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
