using Domain.Entities;
using Domain.ValueObjects;
using Entities;
using NUnit.Framework;
using System;
using ValueObjects;
using ValueObjects.Enums;
using File = Domain.ValueObjects.File;

namespace Tests.Domain.Entities
{
    [TestFixture]
    public class HomeWorkTests
    {
        private const int TestId = 1;
        private Student _testStudent;
        private HomeTask _testHomeTask;
        private TaskCompletionDate _testCompletionDate;
        private Score _testScore;
        private HomeWork _mockHomeWork;

        [SetUp]
        public void Setup()
        {
            // Подготовка тестовых данных
            var studentName = new FullName("Иван", "Иванов");
            var studentPhone = new PhoneNumber("+79001234567");
            var studentEmail = new Email("ivan@example.com");
            var birthDate = new BirthDate(new DateOnly(2000, 1, 10));

            _testStudent = new Student(1, studentName, studentPhone, studentEmail, birthDate);

            var homeTaskName = new HomeTaskName("Домашнее задание 1");
            var duration = new Duration(
                DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now.AddDays(7)));
            _testHomeTask = new HomeTask(1, homeTaskName, "Описание задания", duration);

            _testCompletionDate = new TaskCompletionDate(DateOnly.FromDateTime(DateTime.Now));
            _testScore = new Score(85);
            _mockHomeWork = new HomeWork(_testHomeTask.Id, _testStudent.Id, _testScore, _testCompletionDate, null, "Done homework!", "Good job!", HomeworkStatus.Submitted, true);
        }

        [Test]
        public void Constructor_ShouldCreate_WhenAllParametersValid()
        {
            Assert.That(() => _mockHomeWork, Throws.Nothing);
        }

        // [Test]
        // public void Constructor_ShouldThrow_WhenStudentIsNull()
        // {
        //     Assert.That(
        //         () => new HomeWork(TestId, null, _testHomeTask, _testCompletionDate, _testScore),
        //         Throws.ArgumentNullException.With.Property("ParamName").EqualTo("student"));
        // }

        // [Test]
        // public void Constructor_ShouldThrow_WhenHomeTaskIsNull()
        // {
        //     Assert.That(
        //         () => new HomeWork(TestId, _testStudent, null, _testCompletionDate, _testScore),
        //         Throws.ArgumentNullException.With.Property("ParamName").EqualTo("homeTask"));
        // }

        // [Test]
        // public void Constructor_ShouldThrow_WhenCompletionDateIsNull()
        // {
        //     Assert.That(
        //         () => new HomeWork(TestId, _testStudent, _testHomeTask, null, _testScore),
        //         Throws.ArgumentNullException.With.Property("ParamName").EqualTo("completionDate"));
        // }

        [Test]
        public void Properties_ShouldReturnCorrectValues()
        {
            var homeWork = _mockHomeWork;

            Assert.Multiple(() =>
            {
                Assert.That(homeWork.HomeTaskId, Is.EqualTo(_testHomeTask.Id));
                Assert.That(homeWork.StudentId, Is.EqualTo(_testStudent.Id));
                Assert.That(homeWork.TaskCompletionDate, Is.EqualTo(_testCompletionDate));
                Assert.That(homeWork.Score, Is.EqualTo(_testScore));
                Assert.That(homeWork.Material, Is.Null);
                Assert.That(homeWork.StudentComment, Is.EqualTo("Done homework!"));
                Assert.That(homeWork.TeacherComment, Is.EqualTo("Good job!"));
            });
        }

        // [Test]
        // public void Material_ShouldSetAndGetCorrectly()
        // {
        //     var homeWork = new HomeWork(
        //         TestId,
        //         _testStudent,
        //         _testHomeTask,
        //         _testCompletionDate,
        //         _testScore);

        //     var testFile = new File("path/to/file", "document", "pdf");
        //     homeWork.Material = testFile;

        //     Assert.That(homeWork.Material, Is.EqualTo(testFile));
        // }

        [Test]
        public void Comments_ShouldSetAndGetCorrectly()
        {
            var homeWork = _mockHomeWork;

            homeWork.StudentComment = "Тестовый комментарий студента";
            homeWork.TeacherComment = "Тестовый комментарий преподавателя";

            Assert.Multiple(() =>
            {
                Assert.That(homeWork.StudentComment, Is.EqualTo("Тестовый комментарий студента"));
                Assert.That(homeWork.TeacherComment, Is.EqualTo("Тестовый комментарий преподавателя"));
            });
        }

        // [Test]
        // public void IsSubmittedOnTime_ShouldReturnTrue_WhenSubmittedBeforeDeadline()
        // {
        //     // Используем DateOnly напрямую для создания TaskCompletionDate
        //     var completionDate = new TaskCompletionDate(_testHomeTask.Duration.EndDate);
        //     var homeWork = new HomeWork(
        //         TestId,
        //         _testStudent,
        //         _testHomeTask,
        //         completionDate,
        //         _testScore);

        //     Assert.That(homeWork.IsSubmittedOnTime(), Is.True);
        // }

        // [Test]
        // public void IsSubmittedOnTime_ShouldReturnFalse_WhenSubmittedAfterDeadline()
        // {
        //     // Добавляем 1 день к конечной дате задания
        //     var lateDate = _testHomeTask.Duration.EndDate.AddDays(1);
        //     var completionDate = new TaskCompletionDate(lateDate);

        //     var homeWork = new HomeWork(
        //         TestId,
        //         _testStudent,
        //         _testHomeTask,
        //         completionDate,
        //         _testScore);

        //     Assert.That(homeWork.IsSubmittedOnTime(), Is.False);
        // }

        // [Test]
        // public void CompletionPercentage_ShouldCalculateCorrectly()
        // {
        //     var score = new Score(75);
        //     var homeWork = new HomeWork(
        //         TestId,
        //         _testStudent,
        //         _testHomeTask,
        //         _testCompletionDate,
        //         score);

        //     Assert.That(homeWork.CompletionPercentage, Is.EqualTo(75.0));
        // }

        [Test]
        public void UpdateScore_ShouldUpdateScore_WhenValid()
        {
            var homeWork = _mockHomeWork;

            homeWork.Score = new Score(90);

            Assert.That(homeWork.Score.Value, Is.EqualTo(90));
        }

        // [Test]
        // public void UpdateScore_ShouldThrow_WhenInvalid()
        // {
        //     var homeWork = _mockHomeWork;

        //     Assert.That(
        //         () => homeWork.UpdateScore(150),
        //         Throws.InstanceOf<ArgumentOutOfRangeException>());
        // }

        // [Test]
        // public void AddTeacherComment_ShouldAddFirstComment()
        // {
        //     var homeWork = new HomeWork(
        //         TestId,
        //         _testStudent,
        //         _testHomeTask,
        //         _testCompletionDate,
        //         _testScore);

        //     homeWork.AddTeacherComment("Первый комментарий");

        //     Assert.That(homeWork.TeacherComment, Is.EqualTo("Первый комментарий"));
        // }

        // [Test]
        // public void AddTeacherComment_ShouldAppendNewComment()
        // {
        //     var homeWork = new HomeWork(
        //         TestId,
        //         _testStudent,
        //         _testHomeTask,
        //         _testCompletionDate,
        //         _testScore);

        //     homeWork.AddTeacherComment("Первый комментарий");
        //     homeWork.AddTeacherComment("Второй комментарий");

        //     Assert.That(homeWork.TeacherComment, Is.EqualTo("Первый комментарий\nВторой комментарий"));
        // }
    }
}