using Domain.Entities;
using Domain.ValueObjects;
using Entities;
using NUnit.Framework;
using System;
using System.Linq;
using File = Domain.ValueObjects.File;
using ValueObjects;

namespace Tests.Domain.Entities
{
    [TestFixture]
    public class HomeTaskTests
    {
        private const int TestId = 1;
        private HomeTaskName _validName;
        private string _validDescription;
        private Duration _validDuration;
        private File _testFile;
        private Student _testStudent;
        private TaskCompletionDate _testCompletionDate;

        [SetUp]
        public void Setup()
        {
            _validName = new HomeTaskName("Домашнее задание 1");
            _validDescription = "Описание домашнего задания";
            _validDuration = new Duration(
                DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now.AddDays(7)));
            _testFile = new File("path/to/file", "document", "pdf");

            // Создаем студента с правильными параметрами
            var fullName = new FullName("Иван", "Иванов");
            var phoneNumber = new PhoneNumber("+79001234567");
            var email = new Email("ivan@example.com");
            var birthDate = new BirthDate(new DateOnly(2000, 1, 10));

            _testStudent = new Student(1, fullName, phoneNumber, email, birthDate);

            _testCompletionDate = new TaskCompletionDate(DateOnly.FromDateTime(DateTime.Now));
        }

        [Test]
        public void Constructor_ShouldCreate_WhenAllParametersValid()
        {
            Assert.That(() => new HomeTask(TestId, _validName, _validDescription, _validDuration),
                Throws.Nothing);
        }

        [Test]
        public void Constructor_ShouldThrow_WhenHomeTaskNameIsNull()
        {
            Assert.That(
                () => new HomeTask(TestId, null, _validDescription, _validDuration),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("homeTaskName"));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenDescriptionIsNull()
        {
            Assert.That(
                () => new HomeTask(TestId, _validName, null, _validDuration),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("description"));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenDurationIsDefault()
        {
            Assert.That(
                () => new HomeTask(TestId, _validName, _validDescription, default),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("duration"));
        }

        [Test]
        public void Constructor_ShouldAcceptNullMaterial()
        {
            Assert.That(() => new HomeTask(TestId, _validName, _validDescription, _validDuration, null),
                Throws.Nothing);
        }

        [Test]
        public void Properties_ShouldReturnCorrectValues()
        {
            var homeTask = new HomeTask(TestId, _validName, _validDescription, _validDuration, _testFile);

            Assert.Multiple(() =>
            {
                Assert.That(homeTask.Id, Is.EqualTo(TestId));
                Assert.That(homeTask.HomeTaskName, Is.EqualTo(_validName));
                Assert.That(homeTask.Description, Is.EqualTo(_validDescription));
                Assert.That(homeTask.Duration, Is.EqualTo(_validDuration));
                Assert.That(homeTask.Material, Is.EqualTo(_testFile));
                Assert.That(homeTask.HomeWorks, Is.Empty);
            });
        }

        [Test]
        public void AddHomeWork_ShouldAddWork_WhenValid()
        {
            var homeTask = new HomeTask(TestId, _validName, _validDescription, _validDuration);
            var score = new Score(90);
            var homeWork = new HomeWork(1, _testStudent, homeTask, _testCompletionDate, score);

            homeTask.AddHomeWork(homeWork);

            Assert.That(homeTask.HomeWorks.Count, Is.EqualTo(1));
            Assert.That(homeTask.HomeWorks.First(), Is.EqualTo(homeWork));
        }

        [Test]
        public void AddHomeWork_ShouldThrow_WhenNull()
        {
            var homeTask = new HomeTask(TestId, _validName, _validDescription, _validDuration);

            Assert.That(
                () => homeTask.AddHomeWork(null),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("homeWork"));
        }

        [Test]
        public void AddHomeWork_ShouldThrow_WhenDuplicateId()
        {
            var homeTask = new HomeTask(TestId, _validName, _validDescription, _validDuration);
            var score = new Score(90);
            var homeWork1 = new HomeWork(1, _testStudent, homeTask, _testCompletionDate, score);
            var birthDate = new DateOnly(2000, 1, 10);

            // Создаем нового студента для второй работы
            var anotherStudent = new Student(2,
                new FullName("Петр", "Петров"),
                new PhoneNumber("+79007654321"),
                new Email("petr@example.com"),
                new BirthDate(birthDate));

            var homeWork2 = new HomeWork(1, anotherStudent, homeTask, _testCompletionDate, score);

            homeTask.AddHomeWork(homeWork1);

            Assert.That(
                () => homeTask.AddHomeWork(homeWork2),
                Throws.InvalidOperationException.With.Message.Contain("уже существует"));
        }

        [Test]
        public void RemoveHomeWork_ShouldRemoveWork_WhenExists()
        {
            var homeTask = new HomeTask(TestId, _validName, _validDescription, _validDuration);
            var score = new Score(90);
            var homeWork = new HomeWork(1, _testStudent, homeTask, _testCompletionDate, score);

            homeTask.AddHomeWork(homeWork);
            homeTask.RemoveHomeWork(homeWork);

            Assert.That(homeTask.HomeWorks, Is.Empty);
        }

        [Test]
        public void RemoveHomeWork_ShouldNotThrow_WhenNotExists()
        {
            var homeTask = new HomeTask(TestId, _validName, _validDescription, _validDuration);
            var score = new Score(90);
            var homeWork = new HomeWork(1, _testStudent, homeTask, _testCompletionDate, score);

            Assert.That(() => homeTask.RemoveHomeWork(homeWork), Throws.Nothing);
        }

        [Test]
        public void HomeWorks_ShouldBeReadOnly()
        {
            var homeTask = new HomeTask(TestId, _validName, _validDescription, _validDuration);

            Assert.That(homeTask.HomeWorks,
                Is.InstanceOf<System.Collections.ObjectModel.ReadOnlyCollection<HomeWork>>());
        }
    }
}