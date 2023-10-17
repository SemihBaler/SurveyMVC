using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyMVC.Dtos.AnswerDtos;
using SurveyMVC.Dtos.QuestionDtos;
using SurveyMVC.Dtos.SurveyDtos;

namespace SurveyMVC.Controllers
{
    public class SurveyExampleController : Controller
    {
        private readonly IHttpClientFactory _client;

        public SurveyExampleController(IHttpClientFactory client)
        {
            _client = client;
        }
        public IActionResult Survey()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SurveyPart()
        {
            var questionClient = _client.CreateClient();
            var responseMessage = await questionClient.GetAsync("https://localhost:7132/api/Question/GetByStatusQuestion");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                TempData["resultQuestion"] = json; // JSON olarak sakla
            }

            var answerClient = _client.CreateClient();
            var responseMessage2 = await answerClient.GetAsync("https://localhost:7132/api/Answer/GetByStatusAnswer");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var json = await responseMessage2.Content.ReadAsStringAsync();
                TempData["resultAnswer"] = json; // JSON olarak sakla
            }
            return PartialView();
        }


        [HttpPost]
        public IActionResult Survey([FromBody] SurveyDto surveyDto)
        {
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                if (modelStateEntry.Errors.Any())
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        // Hataları loglayın veya konsola yazdırın
                        Console.WriteLine($"ModelState Error - Field: {key}, Error: {error.ErrorMessage}");
                    }
                }
            }
            return View();
        }

    }
}
