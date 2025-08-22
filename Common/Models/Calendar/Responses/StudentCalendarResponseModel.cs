namespace Common.Models.Calendar.Responses
{
    public class StudentCalendarResponseModel
    {
        public CalendarLessonModel[] CalendarLessonModels { get; set; } = new CalendarLessonModel[0];
        public CalendarHomeTaskModel[] CalendarHomeTaskModels { get; set; } = new CalendarHomeTaskModel[0];
    }
}
