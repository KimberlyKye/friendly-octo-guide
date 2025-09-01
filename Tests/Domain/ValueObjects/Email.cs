using Common.Domain.ValueObjects;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class EmailTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenEmailIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => new Email(string.Empty));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenEmailIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Email(null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenEmailIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => new Email("invalid-email"));
        }

        [Test]
        public void Constructor_ShouldNotThrowException_WhenEmailIsValid()
        {
            Assert.DoesNotThrow(() => new Email("valid-email@example.com"));
        }

        [Test]
        public void Value_ShouldReturnCorrectEmail()
        {
            var email = "valid-email@example.com";
            var emailObject = new Email(email);

            Assert.That(emailObject.Value, Is.EqualTo(email));
        }
    }

}