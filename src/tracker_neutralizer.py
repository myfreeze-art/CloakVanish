import re

def neutralize_trackers(html_content: str) -> str:
    """
    Parses HTML/MIME payloads to execute a heuristic scan, stripping web bugs and tracking pixels.

    Args:
        html_content (str): The raw HTML string to sanitize.

    Returns:
        str: The sanitized HTML string with suspected tracking pixels removed.
    """
    if not html_content:
        return ""

    # Heuristic 1: Remove 1x1 image pixels (common tracking pixel pattern)
    # This matches <img> tags with width and height of 1 (or 0), often used for telemetry
    pixel_pattern = r'<img[^>]*\b(width|height)\s*=\s*["\']?[01](?:px)?["\']?[^>]*\b(width|height)\s*=\s*["\']?[01](?:px)?["\']?[^>]*>'
    sanitized_html = re.sub(pixel_pattern, '', html_content, flags=re.IGNORECASE)

    # Heuristic 2: Remove inline style display:none images that might be tracking pixels
    hidden_img_pattern = r'<img[^>]*style\s*=\s*["\'][^"\']*display\s*:\s*none[^"\']*["\'][^>]*>'
    sanitized_html = re.sub(hidden_img_pattern, '', sanitized_html, flags=re.IGNORECASE)

    # Note: A real-world implementation would use a more robust HTML parser like BeautifulSoup
    # to handle malformed HTML safely, but this demonstrates the algorithmic concept.

    return sanitized_html
