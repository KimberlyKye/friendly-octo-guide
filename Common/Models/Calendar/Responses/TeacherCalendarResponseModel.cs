namespace Common.Models.Calendar.Responses
{
    public class TeacherCalendarResponseModel
    {
        public CalendarLessonModel[] CalendarLessonModels { get; set; } = Array.Empty<CalendarLessonModel>();
    }
}
