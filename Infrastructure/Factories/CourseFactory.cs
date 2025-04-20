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

        public Course CreateFrom(DataModels.Course courseModel, DataModels.User teacher)
        {
            // var state = Enum.TryParse<DataModels.State>(courseModel.State.ToString(), out var parsedState)
            //            ? parsedState
            //            : throw new InvalidOperationException("Неверное состояние курса");

            return new Course(
                id: courseModel.Id,
                teacher: _teacherFactory.CreateFrom(teacher),
                courseName: new CourseName(courseModel.Title),
                description: courseModel.Description,
                duration: new Duration(courseModel.StartDate, courseModel.EndDate)
            // passingScore: courseModel.PassingScore, - y
            // state: state,
            // teacher: _teacherFactory.CreateFrom(courseModel.TeacherId)
            );
        }
    }
}