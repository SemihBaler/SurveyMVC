using Microsoft.AspNetCore.Mvc;

namespace SurveyMVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error403()
        {
            return View();
        }
        public IActionResult Error404()
        {
            return View();
        }
    }
}
