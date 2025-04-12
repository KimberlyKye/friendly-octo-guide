using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Entities;
using NUnit.Framework;
using ValueObjects;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class StudentTests
    {
        private Student _student;

        [SetUp]
        public void SetUp()
        {
            _student = new Student(1, new FullName("John", "Doe"), new PhoneNumber("+79994567890"), new Email("john.doe@example.com"));
        }

        [Test]
        public void AddHomeWork_ShouldAddHomeWorkToList()
        {
            var homeTask = new HomeTask(1, new HomeTaskName("HomeTask1"), "Description", new Duration(new DateOnly(), new DateOnly())); ;
            var homeWork = new HomeWork(1,
                      _student,
                       homeTask,
                      new TaskCompletionDate(new DateOnly()),
                      new Score(100));
            _student.AddHomeWork(homeTask, homeWork);

            var actualHomeWork = _student.GetCourses();

            Assert.IsNotEmpty(actualHomeWork);
            Assert.Contains(homeWork, (System.Collections.ICollection?)actualHomeWork);
            Assert.DoesNotThrow(() => _student.GetListOfHomeWork());
        }

        [Test]
        public void GetListOfHomeWork_ShouldReturnEmptyList()
        {
            var expectedHomeWorks = new List<HomeWork>();
            var actualHomeWorks = _student.GetListOfHomeWork();

            Assert.AreEqual(expectedHomeWorks, actualHomeWorks);
        }

        [Test]
        public void GetStudent_ShouldReturnCorrectStudent()
        {
            var expectedStudent = _student;
            var actualStudent = _student.GetStudent();

            Assert.AreEqual(expectedStudent, actualStudent);
        }
    }

}