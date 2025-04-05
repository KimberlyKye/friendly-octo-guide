using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using NUnit.Framework;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class DateBirthTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenDateIsLessThan8YearsAgo()
        {
            var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-7));
            Assert.Throws<ArgumentException>(() => new BirthDate(date));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenDateIsMoreThan130YearsAgo()
        {
            var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-131));
            Assert.Throws<ArgumentException>(() => new BirthDate(date));
        }

        [Test]
        public void Constructor_ShouldNotThrowException_WhenDateIsBetween8And130YearsAgo()
        {
            var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-19));
            Assert.DoesNotThrow(() => new BirthDate(date));
        }

        [Test]
        public void Date_ShouldReturnCorrectDate()
        {
            var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-19));
            var dateBirth = new BirthDate(date);

            Assert.That(dateBirth.Date, Is.EqualTo(date));
        }
    }

}