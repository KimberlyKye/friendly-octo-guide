using File = Domain.ValueObjects.File;

namespace Tests.Domain.ValueObjects
{
    [TestFixture]
    public class FileTests
    {
        private const string ValidPath = "C:\\Documents";
        private const string ValidName = "document";
        private const string ValidExtension = "pdf";

        [Test]
        public void Constructor_ShouldCreate_WhenAllParametersValid()
        {
            Assert.That(() => new File(ValidPath, ValidName, ValidExtension), Throws.Nothing);
        }

        [Test]
        public void Constructor_ShouldThrow_WhenPathIsNull()
        {
            Assert.That(
                () => new File(null, ValidName, ValidExtension),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("path"));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenNameIsNull()
        {
            Assert.That(
                () => new File(ValidPath, null, ValidExtension),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("name"));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenExtensionIsNull()
        {
            Assert.That(
                () => new File(ValidPath, ValidName, null),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("extension"));
        }

        [Test]
        public void Properties_ShouldReturnCorrectValues()
        {
            var file = new File(ValidPath, ValidName, ValidExtension);

            Assert.That(file.Path, Is.EqualTo(ValidPath));
            Assert.That(file.Name, Is.EqualTo(ValidName));
            Assert.That(file.Extension, Is.EqualTo(ValidExtension));
        }

        [Test]
        public void GetFullPath_ShouldCombinePathNameAndExtension()
        {
            var file = new File(ValidPath, ValidName, ValidExtension);
            var expectedPath = System.IO.Path.Combine(ValidPath, $"{ValidName}.{ValidExtension}");

            Assert.That(file.GetFullPath(), Is.EqualTo(expectedPath));
        }

        [Test]
        public void ToString_ShouldReturnFullPath()
        {
            var file = new File(ValidPath, ValidName, ValidExtension);
            var expectedPath = System.IO.Path.Combine(ValidPath, $"{ValidName}.{ValidExtension}");

            Assert.That(file.ToString(), Is.EqualTo(expectedPath));
        }

        // [Test]
        // public void Equals_ShouldReturnTrue_WhenFilesAreSame()
        // {
        //     var file1 = new File(ValidPath, ValidName, ValidExtension);
        //     var file2 = new File(ValidPath, ValidName, ValidExtension);

        //     Assert.That(file1.Equals(file2), Is.True);
        // }

        [Test]
        public void Equals_ShouldReturnFalse_WhenFilesAreDifferent()
        {
            var file1 = new File(ValidPath, ValidName, ValidExtension);
            var file2 = new File(ValidPath, "other", ValidExtension);

            Assert.That(file1.Equals(file2), Is.False);
        }
    }
}