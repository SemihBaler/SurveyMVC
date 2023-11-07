using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using SurveyMVC.Dtos.AnswerDtos;
using SurveyMVC.Dtos.QuestionDtos;
using SurveyMVC.Dtos.SurveyDtos;
using System.Drawing;
using System.Text;

namespace SurveyMVC.Controllers
{
    [Authorize]
    public class SurveyExampleController : Controller
    {
        private readonly IHttpClientFactory _client;

        public SurveyExampleController(IHttpClientFactory client)
        {
            _client = client;
        }
        public IActionResult Survey()
        {
            if (HttpContext.Response is not null)
            {
                return View();
            }
            return RedirectToAction("Error403", "Error");
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
        public async Task<IActionResult> Survey([FromBody] SurveyDto surveyDto)
        {
            var response = surveyDto.Response.Where(item => item != null).ToList();
            var client = _client.CreateClient();
            for (int i = 0; i < surveyDto.Item.Count; i++)

            {
                if (surveyDto.Response.Count != 0)
                {
                    var result = new ResultDto
                    {
                        Mail = surveyDto.Mail,
                        ResponseDate = surveyDto.ResponseDate,
                        RoomNumber = surveyDto.RoomNumber,
                        Item = surveyDto.Item[i],
                        Response = response[i],
                    };

                    var json = JsonConvert.SerializeObject(result);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var responseMessage = await client.PostAsync("https://localhost:7132/api/Result/AddResult", content);
                }
                else
                {
                    var result = new ResultDto
                    {
                        Mail = surveyDto.Mail,
                        ResponseDate = surveyDto.ResponseDate,
                        RoomNumber = surveyDto.RoomNumber,
                        Item = surveyDto.Item[i],
                        Response = null,
                    };

                    var json = JsonConvert.SerializeObject(result);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var responseMessage = await client.PostAsync("https://localhost:7132/api/Result/AddResult", content);
                }
            }
            return RedirectToAction("Error403", "Error");

        }
    }
}
