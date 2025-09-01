namespace Common.Models.Calendar.Requests
{
    public class GetTeacherCalendarDataRequestModel
    {
        public int TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
