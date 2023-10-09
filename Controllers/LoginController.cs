using Microsoft.AspNetCore.Mvc;
using SurveyMVC.Dtos;

namespace SurveyMVC.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginUserDto user)
        {
            return View();
        }
    }
}
