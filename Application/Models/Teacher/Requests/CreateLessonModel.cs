using Common.Domain.Entities;
using Common.Domain.ValueObjects;

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
        public required Common.Domain.ValueObjects.File? Material { get; init; }
        public HomeTask? HomeTask { get; init; }
    }
}
