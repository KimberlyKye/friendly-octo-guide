using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using File = Domain.ValueObjects.File;

namespace Entities
{
    public class Teacher : Person
    {
        public Teacher(int id, FullName name, PhoneNumber phoneNumber, Email email, BirthDate dateOfBirth)
            : base(id, name, phoneNumber, email, dateOfBirth)
        {
        }

        public Teacher(FullName name, PhoneNumber phoneNumber, Email email, BirthDate dateOfBirth)
            : base(0, name, phoneNumber, email, dateOfBirth)
        {
        }

        public Teacher(int id, FullName name, PhoneNumber phoneNumber, Email email, BirthDate birthDate, string password)
    : base(id, name, phoneNumber, email, birthDate, password)
        {
        }

        public Teacher(FullName name, PhoneNumber phoneNumber, Email email, BirthDate birthDate, string password)
            : base(0, name, phoneNumber, email, birthDate, password)
        {
        }

        public void UpdateCourseInfo(Course course)
        {
            // Реализация метода
        }

        public void UpdateLessonInfo(Lesson lesson, Course course)
        {
            // Реализация метода
        }

        public void SetLessonScore(Teacher student, Lesson lesson)
        {
            // Реализация метода
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            // Реализация метода
        }

        public void CheckHomeWork(HomeWork homeWork, Score score, string comment)
        {
            // Реализация метода
        }

        public Teacher GetTeacher()
        {
            return this;
        }
        public async Task<Course> CreateCourse(int id,
                                               Teacher teacher,
                                               CourseName courseName,
                                               string description,
                                               Duration duration)
        {
            return await Task.Run(() => new Course( id,
                                                    teacher,
                                                    courseName,
                                                    description,
                                                    duration));
        }
        public async Task<Lesson> CreateLesson(int id,
                                               int courseId,
                                               LessonName lessonName,
                                               string description,
                                               DateTime date,
                                               File? material = null,
                                               IEnumerable<HomeTask>? homeTasks = null)
        {
            // Проверки параметров
            if (lessonName == null)
                throw new ArgumentNullException(nameof(lessonName));

            // Доп.проверки???

            return await Task.Run(() => new Lesson( id,
                                                    courseId,
                                                    lessonName,
                                                    description,
                                                    date,
                                                    material,
                                                    homeTasks));
        }
    }
}