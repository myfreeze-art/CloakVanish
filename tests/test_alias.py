import unittest
from src.alias_generator import generate_alias

class TestAliasGenerator(unittest.TestCase):
    def test_generate_alias_format(self):
        alias = generate_alias()

        # Check that it's a string
        self.assertIsInstance(alias, str)

        # Check that it contains a dot separating adjective and noun
        self.assertIn(".", alias)

        # Check that the end is a number
        parts = alias.split(".")
        self.assertEqual(len(parts), 2)

        adjective, noun_and_number = parts[0], parts[1]
        self.assertTrue(adjective.isalpha())

        # Ensure that it ends with a digit
        self.assertTrue(noun_and_number[-1].isdigit())

if __name__ == '__main__':
    unittest.main()
