using Domain.ValueObjects;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Moq;

namespace Infrastructure.Tests.Factories
{
    public class HomeTaskFactoryTests
    {
        private IHomeTaskFactory _homeTaskFactory;
        private Mock<IFileFactory> _fileFactoryMock;

        [SetUp]
        public void Setup()
        {
            _fileFactoryMock = new Mock<IFileFactory>();
            _homeTaskFactory = new HomeTaskFactory(_fileFactoryMock.Object);
        }

        [Test]
        public void HomeTaskFactory_ThrowError_WhenNoFileFactory()
        {
            Assert.Throws<ArgumentNullException>(() => new HomeTaskFactory(null));
        }

        [Test]
        public async Task CreateAsync_ShouldMapHomeTaskCorrectly()
        {
            // Arrange
            var dataModel = new DataModels.HomeTask
            {
                Id = 1,
                Title = "Math Homework",
                Description = "Solve 10 problems",
                StartDate = new DateTime(2024, 10, 1),
                EndDate = new DateTime(2024, 10, 10),
                Material = "math.pdf"
            };

            _fileFactoryMock.Setup(f => f.Create("math.pdf")).Returns(new Domain.ValueObjects.File("math.pdf"));

            // Act
            var result = await _homeTaskFactory.CreateAsync(dataModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dataModel.Id, result.Id);
            Assert.AreEqual(dataModel.Title, result.HomeTaskName.Value);
            Assert.AreEqual(dataModel.Description, result.Description);
            Assert.AreEqual(DateOnly.FromDateTime(dataModel.StartDate), result.Duration.StartDate);
            Assert.AreEqual(DateOnly.FromDateTime(dataModel.EndDate), result.Duration.EndDate);
            Assert.AreEqual("math.pdf", result.Material.Name + "." + result.Material.Extension);
        }

        [Test]
        public async Task CreateDataModelAsync_ShouldMapDomainEntityToHomeTask()
        {
            // Arrange
            var domainEntity = new Entities.HomeTask(
                id: 1,
                homeTaskName: new HomeTaskName("History Assignment"),
                description: "Write an essay",
                duration: new Duration(DateOnly.Parse("2024-10-05"), DateOnly.Parse("2024-10-15")),
                material: new Domain.ValueObjects.File("history.docx")
            );

            _fileFactoryMock.Setup(f => f.GetFullPath(domainEntity.Material))
                            .Returns("C:/materials/history.docx");

            // Act
            var result = await _homeTaskFactory.CreateDataModelAsync(domainEntity);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(domainEntity.Id, result.Id);
            Assert.AreEqual(domainEntity.HomeTaskName.Value, result.Title);
            Assert.AreEqual(domainEntity.Description, result.Description);
            Assert.AreEqual(domainEntity.Duration.StartDate.ToDateTime(TimeOnly.MinValue), result.StartDate);
            Assert.AreEqual(domainEntity.Duration.EndDate.ToDateTime(TimeOnly.MinValue), result.EndDate);
            Assert.AreEqual("C:/materials/history.docx", result.Material);
        }

    }
}