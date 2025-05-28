using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Teacher.Responses
{
    public class TeacherCalendarResponseModel
    {
        public CalendarLessonModel[] CalendarLessonModels { get; set; } = new CalendarLessonModel[0];
    }
}
