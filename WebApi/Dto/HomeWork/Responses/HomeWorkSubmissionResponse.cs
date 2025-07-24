namespace WebApi.Dto.HomeWork.Responses
{
    public class HomeWorkSubmissionResponse
    {
        public int Id { get; set; }
        public int HomeTaskId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
        public DateOnly TaskCompletionDate { get; set; }
        public string? Material { get; set; }
        public string? StudentComment { get; set; }
        public string? TeacherComment { get; set; }
        public string Status { get; set; }
    }
}