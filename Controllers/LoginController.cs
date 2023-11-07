using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SurveyMVC.Dtos;
using SurveyMVC.Dtos.HomeDtos;
using SurveyMVC.Dtos.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SurveyMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _client;

        public LoginController(IHttpClientFactory client)
        {
            _client = client;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginUserDto user)
        {
            var client = _client.CreateClient();
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7132/api/User/LoginUser", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                // Başarılı yanıt alındığında işlemleri yap
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseContent))
                {
                    HttpContext.Session.SetString("token", responseContent);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // Hata işlemlerini burada ele al
                if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }
            // Oturum açma başarısızsa veya hata varsa, aynı sayfaya geri dönün
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("test");
            return RedirectToAction("Index", "Login");
        }
    }
}
