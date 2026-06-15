import random

# Lightweight NLP simulation using word lists to synthesize context-aware, human-like handles
ADJECTIVES = [
    "silent", "swift", "clever", "brave", "calm", "gentle", "fierce", "proud",
    "sharp", "wise", "bold", "dark", "bright", "cool", "warm", "quick"
]

NOUNS = [
    "river", "mountain", "forest", "eagle", "tiger", "wolf", "ocean", "storm",
    "cloud", "shadow", "spark", "flame", "stone", "breeze", "valley", "hawk"
]

def generate_alias() -> str:
    """
    Synthesizes a human-like email handle.
    Returns:
        str: A deterministically generated alias string (e.g., 'silent.river84').
    """
    adjective = random.choice(ADJECTIVES)
    noun = random.choice(NOUNS)
    number = random.randint(10, 99)

    # Format the alias to look like a standard human-created email handle
    alias = f"{adjective}.{noun}{number}"
    return alias

if __name__ == "__main__":
    print(f"Generated Alias: {generate_alias()}")
