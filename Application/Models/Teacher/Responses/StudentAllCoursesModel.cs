using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Teacher.Responses
{
    public class StudentAllCoursesModel
    {
        public int Id { get; set; }
        public CourseName Name { get; set; }
        public bool IsActive { get; set; }
    }
}
