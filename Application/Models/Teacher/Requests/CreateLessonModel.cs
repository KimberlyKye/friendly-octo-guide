using Domain.ValueObjects;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Teacher.Requests
{
    public class CreateLessonModel
    {
        public required int TeacherId { get; init; }
        public required int CourseId { get; init; }
        public required LessonName LessonName { get; init; }
        public required string LessonDescription { get; init; }
        public required DateTime LessonStartDate { get; init; }
        public Score PassingScore { get; init; }
        public required Domain.ValueObjects.File? Material { get; init; }
        public List<HomeTask>? HomeTasks { get; init; }
    }
}
