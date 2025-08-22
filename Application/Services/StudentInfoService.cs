using Application.Models.Course;
using Application.Models.Lesson;
using Application.Models.Teacher.Responses;
using Application.Services.Abstractions;
using Common.Domain.ValueObjects.Enums;
using RepositoriesAbstractions.Abstractions;

namespace Application.Services
{
    public class StudentInfoService : IStudentInfoService
    {
        private readonly IStudentInfoRepository _studentInfoRepository;

        public StudentInfoService(
            IStudentInfoRepository studentInfoRepository)
        {
            _studentInfoRepository = studentInfoRepository;
        }
        public async Task<List<StudentAllCoursesModel>> GetAllCourses(int studentId)
        {
            var courses = await _studentInfoRepository.GetAllCourses(studentId);

            var result = new List<StudentAllCoursesModel>();
            foreach (var course in courses)
            {
                result.Add(new StudentAllCoursesModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    IsActive = course.State == CourseState.Active
                });
            }
            return result;
        }
        public async Task<CourseInfoForStudentModel?> GetCourseInfo(int courseId, int studentId)
        {

            var course = await _studentInfoRepository.GetCourseInfo(courseId, studentId);
            if (course is null) { return null; }

            return new CourseInfoForStudentModel
            {
                State = course.State,
                Teacher = course.Teacher,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration
            };
        }

        public async Task<List<LessonInfoByCourseModel>?> GetLessonsInfoByCourse(int courseId, int studentId)
        {
            var course = await _studentInfoRepository.GetAllCourseInfo(courseId, studentId);
            if (course is null) { return null; }

            return course.Lessons.Select(lesson => new LessonInfoByCourseModel
            {
                CourseId = course.Id,
                LessonName = lesson.Name,
                Description = lesson.Description,
                Date = lesson.Date,
                Material = lesson.Material,
                HomeTask = lesson.HomeTask
            }).ToList();
        }

        public async Task<List<LessonInfoByCourseModel>?> GetHomeworksInfo(int lessonId, int studentId)
        {
            var lesson = await _studentInfoRepository.GetHomeworksInfo(lessonId, studentId);
            if (lesson is null) { return null!; }



            return null;
        }
    }
}
