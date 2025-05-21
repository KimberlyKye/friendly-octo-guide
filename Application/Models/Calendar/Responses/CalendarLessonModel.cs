using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Teacher.Responses
{
    public class CalendarLessonModel
    {  
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public DateTime LessonDate { get; set; }
    }
}
