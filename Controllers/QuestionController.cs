using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SurveyMVC.Dtos;
using SurveyMVC.Dtos.AnswerDtos;
using SurveyMVC.Dtos.QuestionDtos;
using System.Text;
using System.Text.Unicode;

namespace SurveyMVC.Controllers
{
   [Authorize]
    public class QuestionController : Controller
    {
        private readonly IHttpClientFactory _client;

        public QuestionController(IHttpClientFactory client)
        {
            _client = client;
        }
        [HttpGet]
        public async Task<IActionResult> Questions()
        {
            var client = _client.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Question/ListQuestion");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<QuestionDto>>(json);
                return View(result);
            }
            return RedirectToAction("Error403","Error");
        }
        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionDto question)
        {
            var client = _client.CreateClient();
            var json = JsonConvert.SerializeObject(question);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7132/api/Question/AddQuestion", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Questions", "Question");
            }
            return View(question);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateQuestion(int id)
        {
            var client = _client.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Question/GetByIdQuestion?id={id}");
            if(responseMessage.IsSuccessStatusCode)
            {
                var json= await responseMessage.Content.ReadAsStringAsync();
                var value=JsonConvert.DeserializeObject<UpdateQuestionDto>(json);
            
                return View(value);
                
            }
            return RedirectToAction("Error403", "Error");
        }
        [HttpPost]
        public async  Task<IActionResult> UpdateQuestion(UpdateQuestionDto question)
        {
            var client = _client.CreateClient();
            var json=JsonConvert.SerializeObject(question);
            var content=new StringContent(json, Encoding.UTF8,"application/json");
            var responseMessage = await client.PostAsync("https://localhost:7132/api/Question/UpdateQuestion", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Questions", "Question");
            }
            return View(question);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var client= _client.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Question/DeleteQuestion?id={id}");
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Questions", "Question");
            }
            return View(responseMessage);
        }
        [HttpGet]
        public async Task<IActionResult> RemoveQuestion(int id)
        {
            var client = _client.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Question/RemoveQuestion?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Questions");
            }
            return RedirectToAction("Questions", "Question");
        }
    }
}
