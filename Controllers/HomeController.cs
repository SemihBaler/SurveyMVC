using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SurveyMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}