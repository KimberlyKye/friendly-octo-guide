using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class CourseFactory : ICourseFactory
    {
        private readonly ITeacherFactory _teacherFactory;

        public CourseFactory(ITeacherFactory teacherFactory)
        {
            _teacherFactory = teacherFactory;
        }

        public async Task<Course> CreateFrom(
            DataModels.Course courseModel,
            DataModels.User teacher,
            Score? averageScore = null)
        {
            var teacherInfo = await _teacherFactory.CreateFrom(teacher);
            return new Course(
                id: courseModel.Id,
                teacher: teacherInfo,
                courseName: new CourseName(courseModel.Title),
                description: courseModel.Description,
                duration: new Duration(courseModel.StartDate, courseModel.EndDate),
                passingScore: new Score(courseModel.PassingScore),
                averageScore: averageScore
            );
        }

        public DataModels.Course MapTo(Course course)
        {
            return new DataModels.Course()
            {
                StateId = (int)course.State,
                TeacherId = course.Teacher.Id,
                Title = course.Name.Value,
                Description = course.Description,
                StartDate = course.Duration.StartDate,
                EndDate = course.Duration.EndDate,
                PassingScore = course.PassingScore
            };
        }
    }
}