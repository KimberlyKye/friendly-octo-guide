using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Entities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using ValueObjects;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class TeacherTests
    {
        private Teacher _teacher;
        private BirthDate _birthDate;
        private Student _student;

        [SetUp]
        public void SetUp()
        {
            _birthDate = new BirthDate(new DateOnly(1995, 1, 10));
            _teacher = new Teacher(1, new FullName("John", "Doe"), new PhoneNumber("+79994567890"), new Email("john.doe@example.com"), _birthDate);
            _student = new Student(1, new FullName("Jane", "Doe"), new PhoneNumber("+79994567899"), new Email("jane.doe.student@example.com"), _birthDate);
        }

        // [Test]
        // public void UpdateCourseInfo_ShouldUpdateCourseInfo()
        // {
        //     var course = new Course(1, _teacher,
        //                                        new CourseName("name"), "description",
        //                                        new Duration(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 02)));


        //     _teacher.UpdateCourseInfo(course);
        //     var actualCourses = _teacher.GetCourses();

        //     Assert.IsNotEmpty(actualCourses);
        //     Assert.Contains(course, (System.Collections.ICollection?)actualCourses);
        //     // Проверка того, что информация о курсе была обновлена
        // }

        // [Test]
        // public void UpdateLessonInfo_ShouldUpdateLessonInfo()
        // {
        //     var lesson = new Lesson(1,
        //                              1,
        //                              new LessonName("lesson name"),
        //                               "description",
        //                              new DateTime());
        //     var course = new Course(1, _teacher,
        //                             new CourseName("course name"), "description",
        //                             new Duration(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 02)));
        //     _teacher.UpdateLessonInfo(lesson, course);

        //     var actualCourses = _teacher.GetCourses();
        //     var currentCourse = actualCourses.First(c => c.Id.Equals(course.Id));
        //     var lessons = currentCourse.Lessons;
        //     Assert.IsNotEmpty(lessons);
        //     Assert.Contains(lesson, lessons);
        //     // Проверка того, что информация о уроке была обновлена
        // }

        [Test]
        public void SetLessonScore_ShouldSetLessonScore()
        {
            var lesson = new Lesson(1,
                                     1,
                                     new LessonName("lesson name"),
                                      "description",
                                     new DateTime());
            Assert.DoesNotThrow(() => _teacher.SetLessonScore(_teacher, lesson));

            // Проверка того, что оценка за урок была установлена
        }

        [Test]
        public void UpdateHomeTask_ShouldUpdateHomeTask()
        {
            var homeTask = new HomeTask(1, new HomeTaskName("HomeTask1"), "Description", new Duration(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 02)));

            Assert.DoesNotThrow(() => _teacher.UpdateHomeTask(homeTask));

            // Проверка того, что домашнее задание было обновлено
        }

        [Test]
        public void CheckHomeWork_ShouldCheckHomeWork()
        {
            var homeTask = new HomeTask(1, new HomeTaskName("HomeTask1"), "Description", new Duration(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 02)));
            var homeWork = new HomeWork(1,
                                 _student,
                                  homeTask,
                                 new TaskCompletionDate(new DateOnly(2026, 06, 02)),
                                 new Score(100)); var score = new Score(10);
            var comment = "Good job!";
            Assert.DoesNotThrow(() => _teacher.CheckHomeWork(homeWork, score, comment));

            // Проверка того, что домашнее задание было проверено
        }

        [Test]
        public void GetTeacher_ShouldReturnCorrectTeacher()
        {
            var expectedTeacher = _teacher;
            var actualTeacher = _teacher.GetTeacher();

            Assert.AreEqual(expectedTeacher, actualTeacher);
        }
    }

}