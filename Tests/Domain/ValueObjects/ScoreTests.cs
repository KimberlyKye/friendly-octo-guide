namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class ScoreTests
    {
        [Test]
        public void Constructor_ShouldCreate_WhenValueIsValid()
        {
            Assert.That(() => new Score(50), Throws.Nothing);
        }

        [Test]
        public void Constructor_ShouldThrow_WhenValueIsBelowMin()
        {
            Assert.That(
                () => new Score(-1),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With.Message.Contain($"от {Score.MinValue} до {Score.MaxValue}"));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenValueIsAboveMax()
        {
            Assert.That(
                () => new Score(101),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With.Message.Contain($"от {Score.MinValue} до {Score.MaxValue}"));
        }

        [Test]
        public void Constructor_ShouldAcceptMinValue()
        {
            Assert.That(() => new Score(Score.MinValue), Throws.Nothing);
        }

        [Test]
        public void Constructor_ShouldAcceptMaxValue()
        {
            Assert.That(() => new Score(Score.MaxValue), Throws.Nothing);
        }

        [Test]
        public void Value_ShouldReturnCorrectValue()
        {
            const int testValue = 75;
            var score = new Score(testValue);

            Assert.That(score.Value, Is.EqualTo(testValue));
        }

        [Test]
        public void IsValid_ShouldReturnTrue_ForValidValues()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Score.IsValid(0), Is.True);
                Assert.That(Score.IsValid(50), Is.True);
                Assert.That(Score.IsValid(100), Is.True);
            });
        }

        [Test]
        public void IsValid_ShouldReturnFalse_ForInvalidValues()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Score.IsValid(-1), Is.False);
                Assert.That(Score.IsValid(101), Is.False);
            });
        }

        [Test]
        public void ImplicitConversion_ToInt_ShouldReturnValue()
        {
            const int testValue = 85;
            var score = new Score(testValue);

            int value = score;

            Assert.That(value, Is.EqualTo(testValue));
        }

        [Test]
        public void ExplicitConversion_FromInt_ShouldCreateScore()
        {
            const int testValue = 65;

            Score score = (Score)testValue;

            Assert.That(score.Value, Is.EqualTo(testValue));
        }

        [Test]
        public void ExplicitConversion_FromInt_ShouldThrow_WhenInvalid()
        {
            Assert.That(
                () => (Score)150,
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void ToString_ShouldReturnStringRepresentation()
        {
            const int testValue = 90;
            var score = new Score(testValue);

            Assert.That(score.ToString(), Is.EqualTo(testValue.ToString()));
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenValuesAreEqual()
        {
            var score1 = new Score(80);
            var score2 = new Score(80);

            Assert.That(score1.Equals(score2), Is.True);
            Assert.That(score1 == score2, Is.True);
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenValuesAreDifferent()
        {
            var score1 = new Score(70);
            var score2 = new Score(71);

            Assert.That(score1.Equals(score2), Is.False);
            Assert.That(score1 != score2, Is.True);
        }

        [Test]
        public void GetHashCode_ShouldReturnSameValue_ForEqualScores()
        {
            var score1 = new Score(60);
            var score2 = new Score(60);

            Assert.That(score1.GetHashCode(), Is.EqualTo(score2.GetHashCode()));
        }

        [Test]
        public void GetHashCode_ShouldReturnDifferentValue_ForDifferentScores()
        {
            var score1 = new Score(40);
            var score2 = new Score(41);

            Assert.That(score1.GetHashCode(), Is.Not.EqualTo(score2.GetHashCode()));
        }
    }
}