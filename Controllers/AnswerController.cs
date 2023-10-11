using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyMVC.Dtos.AnswerDtos;
using SurveyMVC.Dtos.QuestionDtos;
using System.Text;

namespace SurveyMVC.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IHttpClientFactory _client;

        public AnswerController(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<IActionResult> Answers()
        {
            var client=_client.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Answer/ListAnswer");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<AnswerDto>>(json);
                return View(result);
            }
            return View();
        }
        public IActionResult AddAnswer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAnswer(AddAnswerDto answer)
        {
            var client = _client.CreateClient();
            var json = JsonConvert.SerializeObject(answer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7132/api/Answer/AddAnswer", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Answers", "Answer");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAnswer(int id)
        {
            var client = _client.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Answer/GetByIdAnswer?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAnswerDto>(json);

                return View(value);

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAnswer(UpdateAnswerDto answer)
        {
            var client = _client.CreateClient();
            var json = JsonConvert.SerializeObject(answer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7132/api/Answer/UpdateAnswer", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Answers", "Answer");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var client = _client.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Answer/DeleteAnswer?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Answers", "Answer");
            }
            return View(responseMessage);
        }
        [HttpGet]
        public async Task<IActionResult> RemoveAnswer(int id)
        {
            var client = _client.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7132/api/Answer/RemoveAnswer?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Answers");
            }
            return View(responseMessage);
        }




    }
}
