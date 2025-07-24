using Domain.ValueObjects;
using Entities;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Moq;
using ValueObjects;
using ValueObjects.Enums;
using File = Domain.ValueObjects.File;

namespace Infrastructure.Tests.Factories
{
    public class HomeWorkFactoryTests
    {
        private HomeWorkFactory _homeWorkFactory;
        private Mock<IFileFactory> _fileFactoryMock;

        [SetUp]
        public void Setup()
        {
            _fileFactoryMock = new Mock<IFileFactory>();
            _homeWorkFactory = new HomeWorkFactory(_fileFactoryMock.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldMapHomeworkWithSubmittedStatus()
        {
            // Arrange
            var dataModel = new DataModels.HomeWork
            {
                Id = 1,
                StudentId = 1,
                StudentComment = "I did my best!",
                TaskCompletionDate = DateTime.Now,
                Score = 45,
                Material = "homework.pdf"
            };

            _fileFactoryMock.Setup(f => f.Create("homework.pdf")).Returns(new File("homework.pdf"));

            // Act
            var result = await _homeWorkFactory.CreateAsync(dataModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dataModel.HomeTaskId, result.HomeTaskId);
            Assert.AreEqual(dataModel.StudentId, result.StudentId);
            Assert.AreEqual(dataModel.StudentComment, result.StudentComment);
            Assert.AreEqual(DateOnly.FromDateTime(dataModel.TaskCompletionDate), result.TaskCompletionDate.Value);
            Assert.AreEqual(HomeworkStatus.Submitted, result.Status);
            Assert.IsTrue(result.IsOnTime);
        }

        [Test]
        public async Task CreateAsync_ShouldMapHomeworkWithRejectedStatus()
        {
            // Arrange
            var dataModel = new DataModels.HomeWork
            {
                Id = 1,
                HomeTaskId = 1,
                StudentId = 1,
                StudentComment = "Not good enough.",
                TaskCompletionDate = DateTime.Now,
                Score = 35,
                Material = "homework.pdf"
            };

            _fileFactoryMock.Setup(f => f.Create("homework.pdf")).Returns(new File("homework.pdf"));

            // Act
            var result = await _homeWorkFactory.CreateAsync(dataModel);

            // Assert
            Assert.AreEqual(HomeworkStatus.Rejected, result.Status);
        }

        [Test]
        public async Task CreateAsync_ShouldThrowException_WhenDataModelIsNull()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _homeWorkFactory.CreateAsync(null));
        }

        [Test]
        public async Task CreateAsync_ShouldThrowException_WhenFileCreationFails()
        {
            // Arrange
            var dataModel = new DataModels.HomeWork
            {
                Id = 1,
                StudentId = 1,
                StudentComment = "Some comment",
                TaskCompletionDate = DateTime.Now,
                Score = 50,
                Material = "invalid_material"
            };

            _fileFactoryMock.Setup(f => f.Create("invalid_material")).Throws(new InvalidOperationException());

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => _homeWorkFactory.CreateAsync(dataModel));

            Assert.That(ex.Message, Does.Contain("Error creating HomeWork"));
        }

        [Test]
        public async Task CreateDataModelAsync_ShouldMapDomainEntityToHomeWork()
        {
            // Arrange
            var domainEntity = new HomeWork(
                homeTaskId: 1,
                studentId: 1, score: new Score(70),
                taskCompletionDate: new TaskCompletionDate(DateTime.Now),
                material: new File("math.pdf"),

                studentComment: "Homework done",
                teacherComment: "Well done!",
                status: HomeworkStatus.Submitted,
                isOnTime: true
            );

            _fileFactoryMock.Setup(f => f.GetFullPath(domainEntity.Material)).Returns("C:/materials/math.pdf");

            // Act
            var result = await _homeWorkFactory.CreateDataModelAsync(domainEntity);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(domainEntity.Id, result.Id);
            Assert.AreEqual(domainEntity.StudentId, result.StudentId);
            Assert.AreEqual(domainEntity.StudentComment, result.StudentComment);
            Assert.AreEqual(domainEntity.TaskCompletionDate.Value.ToDateTime(TimeOnly.MinValue), result.TaskCompletionDate);
            Assert.AreEqual(domainEntity.Score.Value, result.Score);
            Assert.AreEqual("C:/materials/math.pdf", result.Material);
        }

        [Test]
        public void CreateDataModelAsync_ShouldThrowException_WhenDomainEntityIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _homeWorkFactory.CreateDataModelAsync(null));
        }
    }
}