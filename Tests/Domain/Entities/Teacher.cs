using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Entities;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class TeacherTests
    {
        private Teacher _teacher;

        [SetUp]
        public void SetUp()
        {
            _teacher = new Teacher(1, new FullName("John", "Doe"), new PhoneNumber("+79994567890"), new Email("john.doe@example.com"));
        }

        [Test]
        public void UpdateCourseInfo_ShouldUpdateCourseInfo()
        {
            var course = new Course();
            Assert.DoesNotThrow(() => _teacher.UpdateCourseInfo(course));

            // Проверка того, что информация о курсе была обновлена
        }

        [Test]
        public void UpdateLessonInfo_ShouldUpdateLessonInfo()
        {
            var lesson = new Lesson();
            Assert.DoesNotThrow(() => _teacher.UpdateLessonInfo(lesson));

            // Проверка того, что информация о уроке была обновлена
        }

        [Test]
        public void SetLessonScore_ShouldSetLessonScore()
        {
            var student = new Student(1, new FullName("Jane", "Doe"), new PhoneNumber("+79994567890"), new Email("jane.doe@example.com"));
            var lesson = new Lesson();
            Assert.DoesNotThrow(() => _teacher.SetLessonScore(student, lesson));

            // Проверка того, что оценка за урок была установлена
        }

        [Test]
        public void UpdateHomeTask_ShouldUpdateHomeTask()
        {
            var homeTask = new HomeTask();
            Assert.DoesNotThrow(() => _teacher.UpdateHomeTask(homeTask));

            // Проверка того, что домашнее задание было обновлено
        }

        [Test]
        public void CheckHomeWork_ShouldCheckHomeWork()
        {
            var homeWork = new HomeWork();
            var score = new Score(10);
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