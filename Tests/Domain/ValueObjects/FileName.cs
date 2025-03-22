using Domain.ValueObjects;

namespace Tests.Domain.ValueObjects
{
    class FileName
    {
        [Test]
        public void FullName_Constructor_ThrowsArgumentNullException_WhenFirstNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FullName(null, "LastName"));
        }

        [Test]
        public void FullName_Constructor_ThrowsArgumentNullException_WhenLastNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FullName("FirstName", null));
        }

        [Test]
        public void FullName_Constructor_ThrowsArgumentException_WhenFirstNameIsTooShort()
        {
            Assert.Throws<ArgumentException>(() => new FullName("A", "LastName"));
        }

        [Test]
        public void FullName_Constructor_ThrowsArgumentException_WhenFirstNameIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new FullName(new string('A', 51), "LastName"));
        }

        [Test]
        public void FullName_Constructor_ThrowsArgumentException_WhenLastNameIsTooShort()
        {
            Assert.Throws<ArgumentException>(() => new FullName("FirstName", "A"));
        }

        [Test]
        public void FullName_Constructor_ThrowsArgumentException_WhenLastNameIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new FullName("FirstName", new string('A', 51)));
        }

        [Test]
        public void FullName_Constructor_ThrowsArgumentException_WhenFirstNameContainsInvalidCharacters()
        {
            Assert.Throws<ArgumentException>(() => new FullName("FirstName123", "LastName"));
        }

        [Test]
        public void FullName_Constructor_ThrowsArgumentException_WhenLastNameContainsInvalidCharacters()
        {
            Assert.Throws<ArgumentException>(() => new FullName("FirstName", "LastName123"));
        }

        [Test]
        public void FullName_GetFullName_ReturnsCorrectFullName()
        {
            var expectedName = "FirstName LastName";
            var fullName = new FullName("FirstName", "LastName");
            Assert.That(fullName.GetFullName(), Is.EqualTo(expectedName));
        }
    }
}
