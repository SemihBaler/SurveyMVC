namespace SurveyMVC.Dtos.SurveyDtos
{
    public class ResultDto
    {
        public int? Item { get; set; }
        public int? Response { get; set; }
        public string RoomNumber { get; set; }
        public string Mail { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
