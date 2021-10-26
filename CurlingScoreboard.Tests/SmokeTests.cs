using System.Drawing;
using System.IO;
using Xunit;

namespace CurlingScoreboard.Tests
{
    public class SmokeTests
    {
        public SmokeTests()
        {

        }
        [Fact]
        public void ImageFile_ShouldExist()
        {
            // Arrange
            string path = PrepareTestFile(nameof(ImageFile_ShouldExist));
            var board = new Board();

            // Act
            board.GenerateImage(path);

            // Assert
            Assert.True(File.Exists(path));
        }

        [Fact]
        public void ImageFile_DefaultShouldBeCertainSize()
        {
            // Arrange
            string path = PrepareTestFile(nameof(ImageFile_DefaultShouldBeCertainSize));
            var board = new Board();

            // Act
            board.GenerateImage(path);

            // Assert
            Image image = Bitmap.FromFile(path);

            Assert.Equal(Board.DefaultWidth, image.Width);
            Assert.Equal(Board.DefaultHeight, image.Height);
        }

        [Fact]
        public void ImageFile_SizeShouldMatchParameters()
        {
            // Arrange
            string path = PrepareTestFile(nameof(ImageFile_SizeShouldMatchParameters));
            var board = new Board(width: 123, height: 456);

            // Act
            board.GenerateImage(path);

            // Assert
            Image image = Bitmap.FromFile(path);

            Assert.Equal(123, image.Width);
            Assert.Equal(456, image.Height);
        }

        private string PrepareTestFile(string testName)
        {
            string path = $"test-image.{testName}.png";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return path;
        }
    }
}
