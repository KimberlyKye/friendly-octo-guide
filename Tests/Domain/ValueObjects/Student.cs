using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using NUnit.Framework;

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
            var homeTask = new HomeTask();
            var homeWork = new HomeWork();
            _student.AddHomeWork(homeTask, homeWork);

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