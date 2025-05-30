using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Teacher.Responses
{
    public class CalendarResponseModel
    {
        public CalendarLessonModel[] CalendarLessonDtos { get; set; } = new CalendarLessonModel[0];
        public CalendarResponseModel[] CalendarHomeTaskDtos { get; set; } = new CalendarResponseModel[0];
    }
}
