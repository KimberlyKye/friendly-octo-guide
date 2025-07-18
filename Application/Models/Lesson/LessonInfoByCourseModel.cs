﻿using Domain.ValueObjects;
using Entities;

namespace Application.Models.Lesson
{
    public class LessonInfoByCourseModel
    {
        public int CourseId { get; set; }
        public LessonName LessonName { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }
        public Domain.ValueObjects.File? Material { get; set; }
        public List<HomeTask> HomeTasks { get; set; }
    }
}
