using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Newtonsoft.Json;
using SurveyMVC.Dtos.HomeDtos;
using SurveyMVC.Dtos.QuestionDtos;
using SurveyMVC.Dtos.SurveyDtos;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SurveyMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _client;

        public HomeController(IHttpClientFactory client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var client = _client.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Result/ListResult");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<HomeDto>>(json);

                return View(result);
            }
            return RedirectToAction("Error403", "Error");
        }

    }
}