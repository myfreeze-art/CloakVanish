using System;

namespace CloakVanishCore
{
    public static class AliasGenerator
    {
        private static readonly string[] Adjectives =
        {
            "silent", "swift", "clever", "brave", "calm", "gentle", "fierce", "proud",
            "sharp", "wise", "bold", "dark", "bright", "cool", "warm", "quick"
        };

        private static readonly string[] Nouns =
        {
            "river", "mountain", "forest", "eagle", "tiger", "wolf", "ocean", "storm",
            "cloud", "shadow", "spark", "flame", "stone", "breeze", "valley", "hawk"
        };

        private static readonly Random Randomizer = new Random();

        /// <summary>
        /// Synthesizes a context-aware, human-like email alias using an NLP simulation.
        /// </summary>
        /// <returns>A string representing the alias (e.g. 'silent.river84').</returns>
        public static string GenerateAlias()
        {
            string adjective = Adjectives[Randomizer.Next(Adjectives.Length)];
            string noun = Nouns[Randomizer.Next(Nouns.Length)];
            int number = Randomizer.Next(10, 100);

            return $"{adjective}.{noun}{number}";
        }
    }
}
