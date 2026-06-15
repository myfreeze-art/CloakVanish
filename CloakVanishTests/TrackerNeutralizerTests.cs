using Xunit;
using CloakVanishCore;

namespace CloakVanishTests
{
    public class TrackerNeutralizerTests
    {
        [Fact]
        public void NeutralizeTrackers_Strips1x1Pixel()
        {
            // Arrange
            string htmlPayload = @"
            <html>
            <body>
                <p>Hello, this is a clean email.</p>
                <img src=""http://tracker.com/pixel.png"" width=""1px"" height=""1px"" alt=""tracker"">
                <img src=""http://tracker.com/pixel2.png"" width=""0"" height=""0"">
            </body>
            </html>";

            // Act
            string sanitized = TrackerNeutralizer.NeutralizeTrackers(htmlPayload);

            // Assert
            Assert.DoesNotContain("<img src=\"http://tracker.com/pixel.png\"", sanitized);
            Assert.DoesNotContain("<img src=\"http://tracker.com/pixel2.png\"", sanitized);
            Assert.Contains("<p>Hello, this is a clean email.</p>", sanitized);
        }

        [Fact]
        public void NeutralizeTrackers_StripsDisplayNonePixel()
        {
            // Arrange
            string htmlPayload = @"
            <html>
            <body>
                <p>Testing display none.</p>
                <img src=""http://tracker.com/hidden.png"" style=""display: none;"">
            </body>
            </html>";

            // Act
            string sanitized = TrackerNeutralizer.NeutralizeTrackers(htmlPayload);

            // Assert
            Assert.DoesNotContain("<img src=\"http://tracker.com/hidden.png\"", sanitized);
            Assert.Contains("<p>Testing display none.</p>", sanitized);
        }

        [Fact]
        public void NeutralizeTrackers_KeepsLegitimateImages()
        {
            // Arrange
            string htmlPayload = @"
            <html>
            <body>
                <p>Look at this cat:</p>
                <img src=""cat.jpg"" width=""500px"" height=""400px"" alt=""Cute Cat"">
            </body>
            </html>";

            // Act
            string sanitized = TrackerNeutralizer.NeutralizeTrackers(htmlPayload);

            // Assert
            Assert.Contains("<img src=\"cat.jpg\"", sanitized);
        }

        [Fact]
        public void NeutralizeTrackers_HandlesEmptyString()
        {
            Assert.Equal(string.Empty, TrackerNeutralizer.NeutralizeTrackers(""));
            Assert.Equal(string.Empty, TrackerNeutralizer.NeutralizeTrackers(null));
        }
    }
}
