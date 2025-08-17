namespace WebApi.Dto.HomeWork.Requests
{
    public class GradeHomeWorkRequest
    {
        public int SubmissionId { get; set; }
        public int? Score { get; set; }
        public string? TeacherComment { get; set; }
    }
}