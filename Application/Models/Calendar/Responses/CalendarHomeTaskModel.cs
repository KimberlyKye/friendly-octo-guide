using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Teacher.Responses
{
    public class CalendarHomeTaskModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public int HomeTaskId { get; set; }
        public string HomeTaskName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
