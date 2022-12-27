# Changelog
All notable changes to this library will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.3.2] - 2022-12-27

### Added

- Unit tests for bidirectional serialization to and from TOML
- Support for serializing nested objects using dot (`.`) notation

### Fixed

- Serialization of arrays and lists containing `null` values
- Serialization of mixed arrays and lists containing nested arrays and lists
- Serialization of mixed dictionaries containing arrays and lists

## [0.3.1] - 2022-12-26

### Fixed

- Serialization order of tables and arrays within another table

## [0.3.0] - 2022-12-26

### Added

- `Deserialize` methods for `TomlSerializer` static class
- Unit tests for `Deserialize` methods
- Unit tests for `Serialize` methods
- Updated documentation

### Changed

- `Serialize` now only serializes `IList` instead of `IEnumerable` fields

### Fixed

- Escape double quotes and backslashes in serialized strings

## [0.2.0] - 2022-12-21

### Added

- `TomlExporter` component for exporting TOML files from Unity scenes
- Documentation for `TomlExporter` component
- Exporter sample scene
- Serializer sample scene

### Fixed

- `TomlSerializer` not putting a new line space between tables

## [0.1.0] - 2022-12-20

### Added

- `TomlSerializer` class for serializing objects to TOML format
- Unit tests for `TomlSerializer` class
- Documentation for `TomlSerializer` class
- Documentation for serialization rules
- Documentation for supported types

