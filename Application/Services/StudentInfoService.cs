using Application.Services.Abstractions;
using Application.Models.Teacher.Responses;
using RepositoriesAbstractions.Abstractions;
using Domain.ValueObjects.Enums;
using Entities;
using Application.Models.Course;
using Domain.ValueObjects;
using Application.Models.Lesson;

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

        public async Task<IEnumerable<int>> AddStudentsToCourse(int courseId, int[] studentIds)
        {
            var filteredStudentIds = new List<int>();

            foreach (var studentId in studentIds)
            {
                if (await _studentInfoRepository.CheckIfUserInCourse(studentId, courseId) != true)
                {
                    filteredStudentIds.Add(studentId);
                }
            }
            await _studentInfoRepository.AddStudentsToCourse(courseId, filteredStudentIds.ToArray());
            return filteredStudentIds;
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

        public async Task<List<Student>> GetAllStudentsByCourse(int courseId)
        {
            var students = await _studentInfoRepository.GetAllStudentsByCourse(courseId);
            return students;
        }

        public async Task<List<Student>> GetAllStudentsOutsideCourse(int courseId, int startRow, int endRow)
        {
            var students = await _studentInfoRepository.GetAllStudentsOutsideCourse(courseId, startRow, endRow);
            return students;
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
            if (course is null) { return null!; }

            return course.Lessons.Select(lesson => new LessonInfoByCourseModel
            {
                CourseId = course.Id,
                LessonName = lesson.Name,
                Description = lesson.Description,
                Date = lesson.Date,
                Material = lesson.Material,
                HomeTasks = lesson.HomeTasks.ToList()
            }).ToList();
        }

        public async Task<IEnumerable<int>> RemoveStudentsFromCourse(int courseId, int[] studentIds)
        {
            // Сначала фильтруем асинхронно
            var filteredStudentIds = new List<int>();

            foreach (var studentId in studentIds)
            {
                if (await _studentInfoRepository.CheckIfUserInCourse(studentId, courseId))
                {
                    filteredStudentIds.Add(studentId);
                }
            }

            await _studentInfoRepository.RemoveStudentsFromCourse(courseId, filteredStudentIds.ToArray());
            return filteredStudentIds;
        }

    }
}
