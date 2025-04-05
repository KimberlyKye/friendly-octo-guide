using Domain.ValueObjects;
using NUnit.Framework;
using System;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class DurationTests
    {
        private readonly DateOnly _validStartDate = new DateOnly(2023, 01, 01);
        private readonly DateOnly _validEndDate = new DateOnly(2023, 12, 31);

        [Test]
        public void Constructor_ShouldCreate_WhenStartDateIsBeforeEndDate()
        {
            Assert.That(() => new Duration(_validStartDate, _validEndDate), Throws.Nothing);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenStartDateEqualsEndDate()
        {
            var date = new DateOnly(2023, 01, 01);
            Assert.That(
                () => new Duration(date, date),
                Throws.ArgumentException.With.Message.Contain("Начальная дата периода должна быть строго меньше"));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenStartDateIsAfterEndDate()
        {
            Assert.That(
                () => new Duration(_validEndDate, _validStartDate),
                Throws.ArgumentException.With.Message.Contain("Начальная дата периода должна быть строго меньше"));
        }

        [Test]
        public void Properties_ShouldReturnCorrectValues()
        {
            var duration = new Duration(_validStartDate, _validEndDate);

            Assert.That(duration.StartDate, Is.EqualTo(_validStartDate));
            Assert.That(duration.EndDate, Is.EqualTo(_validEndDate));
        }

        [Test]
        public void Deconstruct_ShouldReturnCorrectValues()
        {
            var duration = new Duration(_validStartDate, _validEndDate);
            var (startDate, endDate) = duration;

            Assert.That(startDate, Is.EqualTo(_validStartDate));
            Assert.That(endDate, Is.EqualTo(_validEndDate));
        }

        [Test]
        public void Contains_ShouldReturnTrue_WhenDateIsWithinDuration()
        {
            var duration = new Duration(_validStartDate, _validEndDate);
            var testDate = new DateOnly(2023, 06, 15);

            Assert.That(duration.Contains(testDate), Is.True);
        }

        [Test]
        public void Contains_ShouldReturnTrue_WhenDateIsStartDate()
        {
            var duration = new Duration(_validStartDate, _validEndDate);

            Assert.That(duration.Contains(_validStartDate), Is.True);
        }

        [Test]
        public void Contains_ShouldReturnTrue_WhenDateIsEndDate()
        {
            var duration = new Duration(_validStartDate, _validEndDate);

            Assert.That(duration.Contains(_validEndDate), Is.True);
        }

        [Test]
        public void Contains_ShouldReturnFalse_WhenDateIsBeforeStartDate()
        {
            var duration = new Duration(_validStartDate, _validEndDate);
            var testDate = new DateOnly(2022, 12, 31);

            Assert.That(duration.Contains(testDate), Is.False);
        }

        [Test]
        public void Contains_ShouldReturnFalse_WhenDateIsAfterEndDate()
        {
            var duration = new Duration(_validStartDate, _validEndDate);
            var testDate = new DateOnly(2024, 01, 01);

            Assert.That(duration.Contains(testDate), Is.False);
        }

        [Test]
        public void Overlaps_ShouldReturnTrue_WhenDurationsOverlap()
        {
            var duration1 = new Duration(new DateOnly(2023, 01, 01), new DateOnly(2023, 06, 30));
            var duration2 = new Duration(new DateOnly(2023, 06, 01), new DateOnly(2023, 12, 31));

            Assert.That(duration1.Overlaps(duration2), Is.True);
            Assert.That(duration2.Overlaps(duration1), Is.True);
        }

        [Test]
        public void Overlaps_ShouldReturnTrue_WhenOneDurationContainsAnother()
        {
            var duration1 = new Duration(new DateOnly(2023, 01, 01), new DateOnly(2023, 12, 31));
            var duration2 = new Duration(new DateOnly(2023, 06, 01), new DateOnly(2023, 06, 30));

            Assert.That(duration1.Overlaps(duration2), Is.True);
            Assert.That(duration2.Overlaps(duration1), Is.True);
        }

        [Test]
        public void Overlaps_ShouldReturnFalse_WhenDurationsDoNotOverlap()
        {
            var duration1 = new Duration(new DateOnly(2023, 01, 01), new DateOnly(2023, 03, 31));
            var duration2 = new Duration(new DateOnly(2023, 04, 01), new DateOnly(2023, 06, 30));

            Assert.That(duration1.Overlaps(duration2), Is.False);
            Assert.That(duration2.Overlaps(duration1), Is.False);
        }

        [Test]
        public void Overlaps_ShouldReturnTrue_WhenDurationsAreAdjacent()
        {
            var duration1 = new Duration(new DateOnly(2023, 01, 01), new DateOnly(2023, 03, 31));
            var duration2 = new Duration(new DateOnly(2023, 04, 01), new DateOnly(2023, 06, 30));

            Assert.That(duration1.Overlaps(duration2), Is.False);
            Assert.That(duration2.Overlaps(duration1), Is.False);
        }

        [Test]
        public void ToString_ShouldReturnFormattedString()
        {
            var duration = new Duration(new DateOnly(2023, 01, 01), new DateOnly(2023, 12, 31));

            Assert.That(duration.ToString(), Is.EqualTo("2023-01-01 — 2023-12-31"));
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenDurationsAreEqual()
        {
            var duration1 = new Duration(_validStartDate, _validEndDate);
            var duration2 = new Duration(_validStartDate, _validEndDate);

            Assert.That(duration1.Equals(duration2), Is.True);
            Assert.That(duration1 == duration2, Is.True);
            Assert.That(duration1 != duration2, Is.False);
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenDurationsAreDifferent()
        {
            var duration1 = new Duration(_validStartDate, _validEndDate);
            var duration2 = new Duration(_validStartDate.AddDays(1), _validEndDate);

            Assert.That(duration1.Equals(duration2), Is.False);
            Assert.That(duration1 == duration2, Is.False);
            Assert.That(duration1 != duration2, Is.True);
        }

        [Test]
        public void GetHashCode_ShouldReturnSameValue_ForEqualDurations()
        {
            var duration1 = new Duration(_validStartDate, _validEndDate);
            var duration2 = new Duration(_validStartDate, _validEndDate);

            Assert.That(duration1.GetHashCode(), Is.EqualTo(duration2.GetHashCode()));
        }

        [Test]
        public void GetHashCode_ShouldReturnDifferentValue_ForDifferentDurations()
        {
            var duration1 = new Duration(_validStartDate, _validEndDate);
            var duration2 = new Duration(_validStartDate.AddDays(1), _validEndDate);

            Assert.That(duration1.GetHashCode(), Is.Not.EqualTo(duration2.GetHashCode()));
        }
    }
}