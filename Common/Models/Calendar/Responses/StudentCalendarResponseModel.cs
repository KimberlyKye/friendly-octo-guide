namespace Common.Models.Calendar.Responses
{
    public class StudentCalendarResponseModel
    {
        public CalendarLessonModel[] CalendarLessonModels { get; set; } = Array.Empty<CalendarLessonModel>();
        public CalendarHomeTaskModel[] CalendarHomeTaskModels { get; set; } = Array.Empty<CalendarHomeTaskModel>();
    }
}
