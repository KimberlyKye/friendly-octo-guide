namespace WebApi.Dto.HomeWork.Requests
{
    public class HomeWorkSubmissionRequest
    {
        public int StudentId { get; set; }
        public int HomeTaskId { get; set; }
        public string? StudentComment { get; set; }
        public IFormFile? File { get; set; }
    }
}