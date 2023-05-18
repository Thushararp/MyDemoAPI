using DTOs;
using Xunit;

namespace Service.Tests
{
    public class StarRezGameServiceTests
    {
        private readonly StarRezGameService _gameService;

        public StarRezGameServiceTests()
        {
            _gameService = new StarRezGameService();
        }

        #region Unit Tests for GetAll() function
        [Theory]
        [InlineData(1, 5)]
        [InlineData(10, 15)]
        [InlineData(1, 15)]
        public void GetAll_ReturnsCorrectResult(int from, int to)
        {
            // Arrange

            // Act
            var result = _gameService.GetAll(from, to);

            // Assert
            Assert.Equal(to - from + 1, result.Count);

            for (int i = from; i <= to; i++)
            {
                var item = result[i - from];

                if (i % 3 == 0 && i % 5 == 0)
                {
                    Assert.Equal("StarRez", item.Return);
                }
                else if (i % 3 == 0)
                {
                    Assert.Equal("Star", item.Return);
                }
                else if (i % 5 == 0)
                {
                    Assert.Equal("Rez", item.Return);
                }
                else
                {
                    Assert.Equal(i.ToString(), item.Return);
                }
            }
        }
        #endregion

        #region Unit Tests for Validate() function
        [Theory]
        [InlineData(3, "Star")]
        [InlineData(5, "Rez")]
        [InlineData(15, "StarRez")]
        [InlineData(2, "2")]
        public void Validate_ReturnsCorrectResult(int kidIndex, string expectedReturn)
        {
            // Arrange
            var validatePayload = new ValidateDTO
            {
                KidIndex = kidIndex,
                ExpectedReturn = expectedReturn
            };

            // Act
            var result = _gameService.Validate(validatePayload);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_NullPayload_ReturnsFalse()
        {
            // Arrange
            ValidateDTO validatePayload = null;

            // Act
            var result = _gameService.Validate(validatePayload);

            // Assert
            Assert.False(result);
        }
        #endregion
    }
}
