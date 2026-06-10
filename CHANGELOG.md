# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased](https://github.com/jhbelalc/pingt/compare/v2.0.1...HEAD)

### Planned
- Configuration file support
- DNS resolution caching
- Batch ping mode
- JSON output format
- Cross-platform installer

## [2.0.4](https://github.com/jhbelalc/pingt/compare/v2.0.3...v2.0.4) - 2026-06-09

### Added
- Pings counter for each line (ie: `[42]`)
- Counter implemented as `long` to keep several days with no overflow

## [2.0.3](https://github.com/jhbelalc/pingt/compare/v2.0.2...v2.0.3) - 2026-06-07

### Changed
- Migrated from .NET 6.0 to .NET 10.0 LTS
- Updated workflow of CI/CD to compile with .NET 10.0
- Updated badge of .NET in README

## [2.0.2](https://github.com/jhbelalc/pingt/compare/v2.0.1...v2.0.2) - 2026-05-29

### Added
- Displays the session end date and time when CTRL+C is pressed.
- Displays the total execution time (start, end, and duration) before statistics.
- Displays the program version in the header upon startup.

## [2.0.1](https://github.com/jhbelalc/pingt/compare/v2.0.0...v2.0.1) - 2026-05-28

### Fixed
- Fixed error when running the binary on Windows: `hostpolicy.dll` was missing
- Publish method changed to single-file self-contained (`PublishSingleFile=true`)
- Release binaries now include the embedded .NET runtime

### Changed
- Updated CI/CD workflow to generate a single executable per platform
- Removed auxiliary release files (`.dll`, `.deps.json`, `.runtimeconfig.json`)

## [2.0.0] - 2026-05-29

### Added
- Separated concerns with dedicated service classes (PingService, PingStatistics, ConsoleDisplay)
- Async/await pattern for better responsiveness
- Comprehensive statistics tracking (min, max, average, success rate)
- Improved error messages and exception handling
- XML documentation comments for better code understanding
- GitHub Actions workflow for automated builds and releases
- Contributing guidelines and issue templates
- Changelog file

### Changed
- Updated target framework from .NET Core 3.1 to .NET 6.0 LTS
- Refactored Program.cs for better maintainability
- Improved console output formatting and color coding
- Enhanced Ctrl+C handling with proper async cancellation

### Improved
- Code organization and separation of concerns
- Error handling with meaningful messages
- Statistics calculation and tracking
- Console output readability

## [1.0.0] - 2019

### Added
- Initial release
- Basic continuous ping functionality
- Timestamps on each ping result
- Color-coded output (green for success, red for errors)
- Statistics on exit (total pings and errors)
- Support for custom hosts via command-line argument
- Default host (www.google.com) when no argument provided

### Features
- Continuous ping utility
- Timestamp display
- Color-coded results
- Graceful exit with Ctrl+C
- Basic statistics

[Unreleased]: https://github.com/jhbelalc/pingt/compare/v2.0.4...HEAD
[2.0.4]: https://github.com/jhbelalc/pingt/compare/v2.0.3...v2.0.4
[2.0.3]: https://github.com/jhbelalc/pingt/compare/v2.0.2...v2.0.3
[2.0.2]: https://github.com/jhbelalc/pingt/compare/v2.0.1...v2.0.2
[2.0.1]: https://github.com/jhbelalc/pingt/compare/v2.0.0...v2.0.1
[2.0.0]: https://github.com/jhbelalc/pingt/compare/v1.0.0...v2.0.0
[1.0.0]: https://github.com/jhbelalc/pingt/releases/tag/v1.0.0