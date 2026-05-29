# pingt - Continuous Ping Utility with Timestamps

A lightweight command-line tool that continuously pings a host, displaying results with timestamps and statistics. Perfect for monitoring network connectivity without overwhelming your console output.

[![Build and Release](https://github.com/jhbelalc/pingt/actions/workflows/build-and-release.yml/badge.svg?branch=main)](https://github.com/jhbelalc/pingt/actions/workflows/build-and-release.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET Version](https://img.shields.io/badge/.NET-6.0-blue)](https://dotnet.microsoft.com/download/dotnet/6.0)
[![Platform](https://img.shields.io/badge/Platform-Windows%2FmacOS%2FLinux-brightgreen)](https://github.com/jhbelalc/pingt/releases)

## Features

- ✅ **Continuous Ping with Timestamps** - Every ping result includes date and time
- ✅ **Real-time Statistics** - Track success rate, min/max/average response time
- ✅ **Color-coded Output** - Green for success, red for errors
- ✅ **Graceful Shutdown** - Press Ctrl+C to exit and see summary statistics
- ✅ **Cross-platform** - Works on Windows, macOS, and Linux
- ✅ **Custom Hosts** - Ping any host or domain (defaults to google.com)
- ✅ **Clean Code** - Refactored with best practices and separation of concerns

## Screenshots

Original Version:
![pingt_color_code](https://user-images.githubusercontent.com/5776255/84453781-58617600-ac1e-11ea-8dd5-dd40424058ed.png)

## Quick Start

### Option 1: Download Pre-compiled Binary

1. Go to [Releases](https://github.com/jhbelalc/pingt/releases)
2. Download the latest `pingt` executable for your platform
3. Run it directly:
   ```bash
   # Windows
   pingt.exe

   # macOS/Linux
   ./pingt
   ```

### Option 2: Build from Source

**Requirements:**
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later

**Build:**
```bash
# Clone the repository
git clone https://github.com/jhbelalc/pingt.git
cd pingt

# Build the project
dotnet build -c Release

# Run it
dotnet run --project pingt/pingt.csproj

# Or build as self-contained executable
dotnet publish -c Release -o ./publish
./publish/pingt  # macOS/Linux
.\publish\pingt.exe  # Windows
```

## Usage

### Default (ping google.com):
```bash
pingt
```

### Ping a specific host:
```bash
pingt example.com
```

### Examples:
```bash
# Ping a local server
pingt 192.168.1.1

# Ping a specific domain
pingt github.com

# Ping with custom delay (modify source code)
pingt cloudflare.com
```

### Exit the program:
Press `Ctrl+C` to gracefully exit. The program will display final statistics:
```
================================================================================
Ping Statistics: Total: 42 | Success: 41 (97.62%) | Errors: 1 | Min: 25ms | Avg: 42.15ms | Max: 156ms
================================================================================
```

## What's New in v2.0

### Code Improvements
- ✅ Refactored into separate service classes:
  - `PingService` - Handles ping operations
  - `PingStatistics` - Tracks metrics
  - `ConsoleDisplay` - UI/output formatting
  - `Program` - Application entry point
- ✅ Async/await pattern for better responsiveness
- ✅ Proper exception handling with meaningful messages
- ✅ Updated to .NET 6.0 LTS (long-term support)
- ✅ Enabled nullable reference types for type safety
- ✅ Added XML documentation comments
- ✅ Graceful Ctrl+C handling

### Configuration
You can easily modify behavior by editing the constants in `Program.cs`:
```csharp
private static readonly string DefaultHost = "www.google.com";
private static readonly int DefaultTimeout = 1000;  // milliseconds
private static readonly int DefaultDelay = 1000;    // delay between pings
```

Or create a config file (future enhancement).

## Statistics

After pressing Ctrl+C, you'll see a summary:
- **Total Pings** - Number of ping attempts
- **Success** - Successful pings and success rate percentage
- **Errors** - Failed attempts
- **Min/Avg/Max** - Response time statistics

## Development

### Project Structure
```
pingt/
├── Program.cs              # Main entry point
├── PingService.cs          # Ping operation logic
├── PingStatistics.cs       # Statistics tracking
├── ConsoleDisplay.cs       # Console output formatting
└── pingt.csproj           # Project configuration
```

### Building for Distribution

**Windows (Self-contained):**
```bash
dotnet publish -c Release -r win-x64 -o ./dist/windows
```

**macOS (Universal/Intel):**
```bash
dotnet publish -c Release -r osx-x64 -o ./dist/macos
dotnet publish -c Release -r osx-arm64 -o ./dist/macos-arm
```

**Linux:**
```bash
dotnet publish -c Release -r linux-x64 -o ./dist/linux
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

### Ways to Contribute:
1. Report bugs and issues
2. Suggest new features (e.g., configuration file support, DNS resolution caching)
3. Improve documentation
4. Optimize performance
5. Add unit tests

## License

This project is open source and available under the MIT License.

## Author

**John Harold Belalcazar Lozano**
- GitHub: [@jhbelalc](https://github.com/jhbelalc)

## Troubleshooting

### "Command not found" after download
Make sure the file is executable:
```bash
chmod +x pingt  # macOS/Linux
```

### Permission denied
On macOS, you may need to allow the app:
```bash
sudo spctl --add /path/to/pingt
```

### High packet loss
- Check your network connection
- Try pinging a different host
- Check if firewall is blocking ICMP packets

### DNS Resolution Issues
If you can't ping by domain name, try pinging by IP address:
```bash
pingt 8.8.8.8  # Google's DNS
```

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for version history and updates.

### v2.0.0 (2026)
- Complete refactor with separation of concerns
- Updated to .NET 6.0 LTS
- Improved error handling and user experience
- Better statistics tracking
- Added async/await patterns

### v1.0.0 (2019)
- Initial release
- Basic ping functionality with timestamps
- Color-coded output