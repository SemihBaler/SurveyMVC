namespace SurveyMVC.Dtos.QuestionDtos
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string? Item { get; set; }
        public int? Count { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Object Status { get; set; }

    }
}
