import unittest
from src.tracker_neutralizer import neutralize_trackers

class TestTrackerNeutralizer(unittest.TestCase):
    def test_strip_1x1_pixel(self):
        html_payload = '''
        <html>
        <body>
            <p>Hello, this is a clean email.</p>
            <img src="http://tracker.com/pixel.png" width="1px" height="1px" alt="tracker">
            <img src="http://tracker.com/pixel2.png" width="0" height="0">
        </body>
        </html>
        '''
        sanitized = neutralize_trackers(html_payload)

        self.assertNotIn('<img src="http://tracker.com/pixel.png"', sanitized)
        self.assertNotIn('<img src="http://tracker.com/pixel2.png"', sanitized)
        self.assertIn('<p>Hello, this is a clean email.</p>', sanitized)

    def test_strip_display_none_pixel(self):
        html_payload = '''
        <html>
        <body>
            <p>Testing display none.</p>
            <img src="http://tracker.com/hidden.png" style="display: none;">
        </body>
        </html>
        '''
        sanitized = neutralize_trackers(html_payload)

        self.assertNotIn('<img src="http://tracker.com/hidden.png"', sanitized)
        self.assertIn('<p>Testing display none.</p>', sanitized)

    def test_keep_legitimate_images(self):
        html_payload = '''
        <html>
        <body>
            <p>Look at this cat:</p>
            <img src="cat.jpg" width="500px" height="400px" alt="Cute Cat">
        </body>
        </html>
        '''
        sanitized = neutralize_trackers(html_payload)

        self.assertIn('<img src="cat.jpg"', sanitized)

    def test_empty_string(self):
        self.assertEqual(neutralize_trackers(""), "")
        self.assertEqual(neutralize_trackers(None), "")

if __name__ == '__main__':
    unittest.main()
