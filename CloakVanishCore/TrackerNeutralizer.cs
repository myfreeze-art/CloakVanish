using System.Text.RegularExpressions;

namespace CloakVanishCore
{
    public static class TrackerNeutralizer
    {
        /// <summary>
        /// Parses HTML/MIME payloads to execute a heuristic scan, stripping web bugs and tracking pixels.
        /// </summary>
        /// <param name="htmlContent">The raw HTML string to sanitize.</param>
        /// <returns>The sanitized HTML string with suspected tracking pixels removed.</returns>
        public static string NeutralizeTrackers(string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent))
            {
                return string.Empty;
            }

            string sanitizedHtml = htmlContent;

            // Heuristic 1: Remove 1x1 image pixels (common tracking pixel pattern)
            // Matches <img> tags with width and height of 1 (or 0), often used for telemetry
            string pixelPattern = @"<img[^>]*\b(width|height)\s*=\s*[""']?[01](?:px)?[""']?[^>]*\b(width|height)\s*=\s*[""']?[01](?:px)?[""']?[^>]*>";
            sanitizedHtml = Regex.Replace(sanitizedHtml, pixelPattern, string.Empty, RegexOptions.IgnoreCase);

            // Heuristic 2: Remove inline style display:none images that might be tracking pixels
            string hiddenImgPattern = @"<img[^>]*style\s*=\s*[""'][^""']*display\s*:\s*none[^""']*[""'][^>]*>";
            sanitizedHtml = Regex.Replace(sanitizedHtml, hiddenImgPattern, string.Empty, RegexOptions.IgnoreCase);

            return sanitizedHtml;
        }
    }
}
