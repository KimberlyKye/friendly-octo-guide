using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Entities;
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

        public async Task<Course> CreateFrom(DataModels.Course courseModel, DataModels.User teacher)
        {
            var teacherInfo = await _teacherFactory.CreateFrom(teacher);
            return new Course(
                id: courseModel.Id,
                teacher: teacherInfo,
                courseName: new CourseName(courseModel.Title),
                description: courseModel.Description,
                duration: new Duration(courseModel.StartDate, courseModel.EndDate)
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
                PassingScore = 0 // Значение по умолчанию, требуется доработка при добавлении PassingScore в домен
            };
        }
    }
}