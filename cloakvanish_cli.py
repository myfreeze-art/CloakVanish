import argparse
import sys
import os

from src.alias_generator import generate_alias
from src.tracker_neutralizer import neutralize_trackers

def main():
    parser = argparse.ArgumentParser(
        description="CloakVanish CLI - Autonomous Zero-Knowledge Communication Proxy Interface",
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Examples:
  python cloakvanish_cli.py generate-alias
  python cloakvanish_cli.py neutralize-html --file email.html
        """
    )

    subparsers = parser.add_subparsers(dest="command", help="Available commands")

    # Command: generate-alias
    alias_parser = subparsers.add_parser("generate-alias", help="Synthesize a context-aware, human-like email alias")

    # Command: neutralize-html
    neutralize_parser = subparsers.add_parser("neutralize-html", help="Strip telemetry beacons and tracking pixels from an HTML file")
    neutralize_parser.add_argument("--file", type=str, required=True, help="Path to the HTML file to process")
    neutralize_parser.add_argument("--output", type=str, help="Optional: Path to save the sanitized output (defaults to stdout)")

    args = parser.parse_args()

    if args.command == "generate-alias":
        alias = generate_alias()
        print(f"[+] Deterministic Alias Generated: {alias}@cv-mail.node")
        sys.exit(0)

    elif args.command == "neutralize-html":
        if not os.path.exists(args.file):
            print(f"[-] Error: File '{args.file}' not found.")
            sys.exit(1)

        try:
            with open(args.file, 'r', encoding='utf-8') as f:
                html_content = f.read()
        except Exception as e:
            print(f"[-] Error reading file: {e}")
            sys.exit(1)

        print(f"[*] Processing '{args.file}' through heuristic tracker neutralization...")
        sanitized_html = neutralize_trackers(html_content)

        if args.output:
            try:
                with open(args.output, 'w', encoding='utf-8') as f:
                    f.write(sanitized_html)
                print(f"[+] Sanitized HTML saved to '{args.output}'")
            except Exception as e:
                print(f"[-] Error writing output file: {e}")
                sys.exit(1)
        else:
            print("\n[+] Sanitized HTML Output:\n")
            print(sanitized_html)
            print("\n[+] End of Output")

        sys.exit(0)

    else:
        parser.print_help()
        sys.exit(1)

if __name__ == "__main__":
    main()
