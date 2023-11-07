namespace SurveyMVC.Dtos.HomeDtos
{
    public class HomeDto
    {
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public string RoomNumber { get; set; }
        public string Mail { get; set; }
        public DateTime CreatedDate { get; set; }
        public string QuestionItem { get; set; }
        public string AnswerItem { get; set; }
    }
}
