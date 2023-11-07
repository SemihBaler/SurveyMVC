namespace SurveyMVC.Dtos.AnswerDtos
{
    public class AnswerDto
    {
        public int AnswerId { get; set; }
        public string? Response { get; set; }
        public int? Count { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Object Status { get; set; }
    }
}
