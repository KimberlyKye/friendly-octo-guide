namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class LessonNameTests
    {
        private const string ValidName = "Основы C sharp: структуры данных";
        private const string LongName = "Это очень длинное название урока, которое явно превышает максимально допустимую длину в сто символов и поэтому должно вызывать ошибку валидации при попытке создания";

        [Test]
        public void Constructor_ShouldCreate_WhenNameIsValid()
        {
            Assert.That(() => new LessonName(ValidName), Throws.Nothing);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull()
        {
            Assert.That(
                () => new LessonName(null),
                Throws.ArgumentNullException
                    .With.Property("ParamName").EqualTo("name")
                    .And.Message.Contain("не может быть пустым"));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenNameIsEmpty()
        {
            Assert.That(
                () => new LessonName(""),
                Throws.ArgumentNullException
                    .With.Property("ParamName").EqualTo("name"));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenNameIsWhitespace()
        {
            Assert.That(
                () => new LessonName("   "),
                Throws.ArgumentNullException
                    .With.Property("ParamName").EqualTo("name"));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsTooShort()
        {
            Assert.That(
                () => new LessonName("A"),
                Throws.ArgumentException
                    .With.Message.Contain($"минимум {LessonName.MinLength}"));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenNameIsTooLong()
        {
            Assert.That(
                () => new LessonName(LongName),
                Throws.ArgumentException
                    .With.Message.Contain($"максимум {LessonName.MaxLength}"));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenNameContainsInvalidChars()
        {
            Assert.That(
                () => new LessonName("C# @*&"),
                Throws.ArgumentException
                    .With.Message.Contain("только буквы, цифры, пробелы и ,.-:;!?()"));
        }

        [Test]
        public void Constructor_ShouldNormalizeName_TrimSpaces()
        {
            var nameWithSpaces = "   Введение в ООП   ";
            var lessonName = new LessonName(nameWithSpaces);

            Assert.That(lessonName.Value, Is.EqualTo("Введение в ООП"));
        }

        [Test]
        public void Value_ShouldReturnCorrectName()
        {
            var lessonName = new LessonName(ValidName);

            Assert.That(lessonName.Value, Is.EqualTo(ValidName));
        }

        [Test]
        public void Value_Set_ShouldUpdateName_WhenValid()
        {
            var lessonName = new LessonName(ValidName);
            var newName = "Паттерны проектирования";

            lessonName.Value = newName;

            Assert.That(lessonName.Value, Is.EqualTo(newName));
        }

        [Test]
        public void Value_Set_ShouldThrow_WhenInvalid()
        {
            var lessonName = new LessonName(ValidName);

            Assert.That(
                () => lessonName.Value = "A",
                Throws.ArgumentException
                    .With.Message.Contain($"минимум {LessonName.MinLength}"));
        }

        [Test]
        public void ToString_ShouldReturnName()
        {
            var lessonName = new LessonName(ValidName);

            Assert.That(lessonName.ToString(), Is.EqualTo(ValidName));
        }

        [Test]
        public void ImplicitConversion_ShouldReturnName()
        {
            var lessonName = new LessonName(ValidName);

            string name = lessonName;

            Assert.That(name, Is.EqualTo(ValidName));
        }

        [Test]
        public void ExplicitConversion_ShouldCreateLessonName()
        {
            LessonName lessonName = (LessonName)ValidName;

            Assert.That(lessonName.Value, Is.EqualTo(ValidName));
        }

        [Test]
        public void ExplicitConversion_ShouldThrow_WhenInvalid()
        {
            Assert.That(
                () => (LessonName)"A",
                Throws.ArgumentException);
        }

        // [Test]
        // public void Equals_ShouldReturnTrue_WhenNamesAreEqual()
        // {
        //     var name1 = new LessonName(ValidName);
        //     var name2 = new LessonName(ValidName);

        //     Assert.That(name1.Equals(name2), Is.True);
        // }

        [Test]
        public void Equals_ShouldReturnFalse_WhenNamesAreDifferent()
        {
            var name1 = new LessonName(ValidName);
            var name2 = new LessonName("Другое название");

            Assert.That(name1.Equals(name2), Is.False);
        }

        [Test]
        public void ValidCharsRegex_ShouldAllowPunctuation()
        {
            var namesWithPunctuation = new[]
            {
                "Введение: основы",
                "Списки, кортежи, словари",
                "Строки (работа с текстом)",
                "Методы - лучшие практики!",
                "Что такое ООП?"
            };

            foreach (var name in namesWithPunctuation)
            {
                Assert.That(() => new LessonName(name), Throws.Nothing,
                    $"Не удалось создать LessonName с допустимым названием: '{name}'");
            }
        }
    }
}