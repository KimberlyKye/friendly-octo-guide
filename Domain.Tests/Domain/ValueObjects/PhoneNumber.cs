using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class PhoneNumberTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenPhoneNumberIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => new PhoneNumber(string.Empty));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenPhoneNumberIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PhoneNumber(null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenPhoneNumberIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => new PhoneNumber("invalid-phone-number"));
        }

        [Test]
        public void Constructor_ShouldNotThrowException_WhenPhoneNumberIsValid()
        {
            Assert.DoesNotThrow(() => new PhoneNumber("+79001234567"));
        }

        [Test]
        public void Value_ShouldReturnCorrectPhoneNumber()
        {
            var phoneNumber = "+79001234567";
            var phoneNumberObject = new PhoneNumber(phoneNumber);

            Assert.AreEqual(phoneNumber, phoneNumberObject.Value);
        }

        [Test]
        public void ToString_ShouldReturnCorrectPhoneNumber()
        {
            var phoneNumber = "+79001234567";
            var phoneNumberObject = new PhoneNumber(phoneNumber);

            Assert.AreEqual(phoneNumber, phoneNumberObject.ToString());
        }

        [Test]
        public void ImplicitConversion_ShouldReturnCorrectPhoneNumber()
        {
            var phoneNumber = "+79001234567";
            var phoneNumberObject = new PhoneNumber(phoneNumber);

            string convertedPhoneNumber = phoneNumberObject;

            Assert.AreEqual(phoneNumber, convertedPhoneNumber);
        }
    }

}