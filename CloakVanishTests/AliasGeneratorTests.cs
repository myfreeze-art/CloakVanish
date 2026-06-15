using Xunit;
using CloakVanishCore;

namespace CloakVanishTests
{
    public class AliasGeneratorTests
    {
        [Fact]
        public void GenerateAlias_ShouldReturnValidFormat()
        {
            // Act
            string alias = AliasGenerator.GenerateAlias();

            // Assert
            Assert.NotNull(alias);
            Assert.Contains(".", alias);

            string[] parts = alias.Split('.');
            Assert.Equal(2, parts.Length);

            // Adjective should only contain letters
            Assert.Matches("^[a-zA-Z]+$", parts[0]);

            // Noun+Number should end in digits
            Assert.Matches("^[a-zA-Z]+[0-9]{2}$", parts[1]);
        }
    }
}
