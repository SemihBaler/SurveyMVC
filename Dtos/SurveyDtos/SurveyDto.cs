using System.Text.Json;
using System.Text.Json.Serialization;

namespace SurveyMVC.Dtos.SurveyDtos
{
    public class SurveyDto
    {
        public List<int?> Item { get; set; }
        public List<int?> Response { get; set; }
        public string RoomNumber { get; set; }
        public string Mail { get; set; }
        public DateTime ResponseDate { get; set; }

    }
}
