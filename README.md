# CloakVanish (CV-Mail Protocol)

**System Designation:** Autonomous Zero-Knowledge Communication Proxy

CloakVanish is an API-driven, zero-knowledge email routing and sanitization platform designed to eliminate metadata leakage and prevent identity correlation. Operating on a decentralized server infrastructure, the protocol abstracts the user’s true identity by inserting an untraceable, algorithmic buffer between the sender and the recipient. It requires zero cryptographic key-sharing or personal telemetry during initialization.

## Core Algorithmic Features

*   **Deterministic Alias Generation:** Instead of generating pseudo-random strings that trigger anti-bot heuristics, CloakVanish utilizes a lightweight natural language processing (NLP) model to synthesize context-aware, human-like email handles. These aliases seamlessly pass through strict fraud-detection gates.
*   **Heuristic Tracker Neutralization:** The platform’s inbound proxy parses all incoming HTML and MIME payloads. It executes a real-time heuristic scan to isolate and strip telemetry beacons, web bugs, and tracking pixels, effectively rendering fingerprinting scripts completely inert before server-side caching.
*   **Asymmetric Payload Encryption:** Every message hitting the CloakVanish node is instantly encrypted at the ingestion layer using ephemeral X25519 key pairs. Decryption occurs strictly client-side; data residing on the servers exists purely as high-entropy noise, ensuring total immunity against third-party data breaches.
*   **Automated Ephemeral Purging (Cron-Shred):** Upon reaching user-defined TTL (Time-To-Live) thresholds, the system triggers a multi-pass cryptographic wipe across all distributed storage nodes, leaving absolutely no forensic remnants of the inbox or its metadata.
*   **Anonymized Outbound Relay:** The outbound SMTP relay reconstructs message headers from scratch, completely purging originating IP addresses, user-agent data, and mail client routing histories to guarantee unidirectional anonymity during replies.

## Project Architecture (C# / .NET)

This repository provides a fully functional native Windows Desktop Application demonstrating the core conceptual logic of CloakVanish. The solution is built using **C#**, **.NET**, and **WPF (Windows Presentation Foundation)**.

```
CloakVanish/
├── CloakVanish.sln                   # The main .NET Solution file
├── CloakVanishCore/                  # C# Class Library (Core Logic)
│   ├── AliasGenerator.cs             # Synthesizes human-like aliases
│   └── TrackerNeutralizer.cs         # Strips tracking pixels using Regex
├── CloakVanishApp/                   # WPF Desktop Application (Native UI)
│   ├── MainWindow.xaml               # Application layout (XAML)
│   └── MainWindow.xaml.cs            # UI Event handlers connecting to Core
└── CloakVanishTests/                 # xUnit Test Suite
    ├── AliasGeneratorTests.cs
    └── TrackerNeutralizerTests.cs
```

## Setup & Deployment

### Prerequisites

*   A Windows Operating System.
*   [.NET SDK 8.0+](https://dotnet.microsoft.com/download) (or the version specified by your environment).
*   Visual Studio 2022 (Recommended) OR Visual Studio Code with the C# Dev Kit.

### Building and Running Locally

#### Option 1: Using Visual Studio
1. Clone the repository: `git clone https://github.com/your-org/cloakvanish.git`
2. Open the `CloakVanish.sln` file in Visual Studio.
3. Ensure `CloakVanishApp` is set as the Startup Project.
4. Press `F5` or click **Start** to build and run the native WPF application.

#### Option 2: Using the .NET CLI
1. Clone the repository and navigate to the root directory.
2. Restore dependencies:
   ```cmd
   dotnet restore
   ```
3. Build the solution:
   ```cmd
   dotnet build
   ```
4. Run the WPF application natively:
   ```cmd
   dotnet run --project CloakVanishApp
   ```

### Running the Test Suite
The core algorithms are fully tested using xUnit. You can verify the project's logic by running:
```cmd
dotnet test CloakVanishTests
```

### Compiling to a Standalone Executable (.exe)
You can publish the WPF application as a single, standalone executable that doesn't require users to install the .NET runtime:
```cmd
dotnet publish CloakVanishApp -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```
The resulting `.exe` will be generated in `CloakVanishApp/bin/Release/netX.X/win-x64/publish/`.
