using Domain.ValueObjects;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class CourseNameTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenNameIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => new CourseName(string.Empty));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CourseName(null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsTooShort()
        {
            Assert.Throws<ArgumentException>(() => new CourseName("A"));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsTooLong()
        {
            var longName = new string('A', 51); // Создаем строку длиной 51 символ
            Assert.Throws<ArgumentException>(() => new CourseName(longName));
        }

        [Test]
        public void Constructor_ShouldNotThrowException_WhenNameIsValid()
        {
            Assert.DoesNotThrow(() => new CourseName("Valid Course Name"));
        }

        [Test]
        public void Constructor_ShouldNotThrowException_WhenNameIsMinLength()
        {
            Assert.DoesNotThrow(() => new CourseName("AB"));
        }

        [Test]
        public void Constructor_ShouldNotThrowException_WhenNameIsMaxLength()
        {
            var maxLengthName = new string('A', 50);
            Assert.DoesNotThrow(() => new CourseName(maxLengthName));
        }

        [Test]
        public void Name_ShouldReturnCorrectValue()
        {
            var courseName = "Test Course";
            var courseNameObject = new CourseName(courseName);

            Assert.AreEqual(courseName, courseNameObject.Name);
        }

        [Test]
        public void ToString_ShouldReturnCorrectName()
        {
            var courseName = "Test Course";
            var courseNameObject = new CourseName(courseName);

            Assert.AreEqual(courseName, courseNameObject.ToString());
        }

        [Test]
        public void ImplicitConversion_ShouldReturnCorrectName()
        {
            var courseName = "Test Course";
            var courseNameObject = new CourseName(courseName);

            string convertedName = courseNameObject;

            Assert.AreEqual(courseName, convertedName);
        }
    }
}