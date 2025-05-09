using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Teacher.Responses
{
    public class CalendarHomeTaskModel
    {
        public CalendarLessonModel[] CalendarLessonDtos { get; set; } = new CalendarLessonModel[0];
        public CalendarHomeTaskModel[] CalendarHomeTaskDtos { get; set; } = new CalendarHomeTaskModel[0];
    }
}
