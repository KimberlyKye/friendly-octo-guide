using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;
using NUnit.Framework;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class PersonTests
    {
        private Person _person;

        [SetUp]
        public void SetUp()
        {
            _person = new Person(1, new FullName("John", "Doe"), new PhoneNumber("+79994567890"), new Email("john.doe@example.com"));
        }

        [Test]
        public void GetName_ShouldReturnCorrectName()
        {
            var expectedName = new FullName("John", "Doe");
            var actualName = _person.GetName();

            Assert.That(actualName, Has.Property("FirstName").EqualTo(expectedName.FirstName));
            Assert.That(actualName, Has.Property("LastName").EqualTo(expectedName.LastName));
        }


        [Test]
        public void GetPhoneNumber_ShouldReturnCorrectPhoneNumber()
        {
            var expectedPhoneNumber = new PhoneNumber("+79994567890");
            var actualPhoneNumber = _person.GetPhoneNumber();

            Assert.That(actualPhoneNumber, Has.Property("Value").EqualTo(expectedPhoneNumber.Value));
        }


        [Test]
        public void GetEmail_ShouldReturnCorrectEmail()
        {
            var expectedEmail = new Email("john.doe@example.com");
            var actualEmail = _person.GetEmail();

            Assert.That(actualEmail, Has.Property("Value").EqualTo(expectedEmail.Value));
        }

        [Test]
        public void GetDateOfBirth_ShouldReturnCorrectDateOfBirth()
        {
            var expectedDateOfBirth = new BirthDate(new DateOnly(1990, 1, 1));
            _person.SetDateOfBirth(expectedDateOfBirth);
            var actualDateOfBirth = _person.GetDateOfBirth();

            Assert.That(actualDateOfBirth, Is.EqualTo(expectedDateOfBirth));
        }

        [Test]
        public void GetCourses_ShouldReturnEmptyList()
        {
            var expectedCourses = new List<Course>();
            var actualCourses = _person.GetCourses();

            Assert.That(actualCourses, Is.EqualTo(expectedCourses));
        }

        [Test]
        public void SetCourse_ShouldAddCourseToList()
        {
            Assert.DoesNotThrow(() => _person.SetCourse(new Course()));
        }

        [Test]
        public void RemoveCourse_ShouldRemoveCourseFromList()
        {
            var course = new Course();
            _person.SetCourse(course);
            _person.RemoveCourse(course);
            var actualCourses = _person.GetCourses();

            Assert.IsEmpty(actualCourses);
        }

        [Test]
        public void SetPhoneNumber_ShouldSetCorrectPhoneNumber()
        {
            var expectedPhoneNumber = new PhoneNumber("+79994567890");
            _person.SetPhoneNumber(expectedPhoneNumber);
            var actualPhoneNumber = _person.GetPhoneNumber();

            Assert.That(actualPhoneNumber, Is.EqualTo(expectedPhoneNumber));
        }

        [Test]
        public void SetEmail_ShouldSetCorrectEmail()
        {
            var expectedEmail = new Email("jane.doe@example.com");
            _person.SetEmail(expectedEmail);
            var actualEmail = _person.GetEmail();

            Assert.That(actualEmail, Is.EqualTo(expectedEmail));
        }

        [Test]
        public void SetDateOfBirth_ShouldSetCorrectDateOfBirth()
        {
            var expectedDateOfBirth = new BirthDate(new DateOnly(1991, 1, 1));
            _person.SetDateOfBirth(expectedDateOfBirth);
            var actualDateOfBirth = _person.GetDateOfBirth();

            Assert.That(actualDateOfBirth, Is.EqualTo(expectedDateOfBirth));
        }
    }

}