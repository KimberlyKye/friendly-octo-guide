namespace Common.Models.Calendar.Requests
{
    public class GetStudentCalendarDataRequestModel
    {
        public int StudentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
