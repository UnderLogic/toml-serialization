# Changelog
All notable changes to this library will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.4.9] - 2023-01-13

### Fixed

- `TomlInlineAttribute` now properly applied to complex types during serialization

## [0.4.8] - 2023-01-13

### Fixed

- Nested table and table array keys are now properly prefixed with the parent table key when serialized
- Empty table keys are no longer serialized (as they are not necessary)
- Nested tables and table arrays are now properly deserialized

## [0.4.7] - 2023-01-09

### Fixed

- Escape whitespace strings in non-multiline strings
- Escape `\f` form-feed character in strings
- Unescape whitespace chars in character and string arrays
- All code is now C# 7.3 and .NET Framework 4.6 compatible for Unity 2019.1+

## [0.4.6] - 2023-01-04

### Added

- `TomlCamelCaseAttribute` custom attribute
- `TomlPascalCaseAttribute` custom attribute
- `TomlSnakeCaseAttribute` custom attribute (allow for uppercase)
- `TomlKebabCaseAttribute` custom attribute

### Removed

- `TomlCasingAttribute` custom attribute in favor of specific ones

## [0.4.5] - 2023-01-04

### Added

- `TomlHexNumberAttribute` custom attribute
- `TomlOctalNumberAttribute` custom attribute
- `TomlBinaryNumberAttribute` custom attribute
- `TomlDateTimeFormatAttribute` custom attribute

### Removed

- `TomlNumberFormatAttribute` custom attribute in favor of specific ones

## [0.4.4] - 2023-01-02

### Added

- Serialization support for `PositiveInfinity`, `NegativeInfinity` and `NaN` values
- Deserialization support for `PositiveInfinity`, `NegativeInfinity` and `NaN` values
- `TomlNumberFormatAttribute` for serializing integer values in different formats
- Deserialize `hex` integer values (e.g. `0xdead_beef`)
- Deserialize `oct` integer values (e.g. `0o755`)
- Deserialize `bin` integer values (e.g. `0b1101010`)
- Unit tests for number formats and digit separators (e.g. `1_000_000`)

## [0.4.3] - 2022-12-31

### Added

- `TomlMultilineAttribute` can be applied to arrays and lists to serialize them as multiline TOML arrays
- `TomlLiteralAttribute` can be applied to lists of strings to serialize them as TOML literal strings
- `TomlLiteralAttribute` can be applied to dictionaries of strings to serialize them as TOML literal strings

## [0.4.2] - 2022-12-31

### Added

- Deserialization support for multiline arrays (including jagged arrays)

### Fixed

- Allow trailing commas for arrays

## [0.4.1] - 2022-12-30

### Added

- `TomlLiteralAttribute` custom attribute for serializing literal strings
- `TomlMultilineAttribute` custom attribute for serializing multi-line strings (including literals)
- `TomlInlineAttribute` custom attribute for serializing inline tables explicitly
- `TomlExpandAttribute` custom attribute for serializing expanded tables explicitly
- More unit tests for various serialization cases

### Changed

- `TomlFloat` values can be coerced into integer values (will be truncated)

## [0.4.0] - 2022-12-30

### Added

- `TomlKeyAttribute` custom attribute for overriding the TOML key of a field
- `TomlCasingAttribute` custom attribute for overriding the TOML key casing of an object or field

### Fixed

- Excessive escaping of strings in TOML output

## [0.3.5] - 2022-12-29

### Added

- Deserialization of multiline basic strings using `"""` syntax
- Deserialization of multiline literal strings using `'''` syntax
- Deserialization of unicode escape sequences in basic strings using `\uXXXX` syntax
- More documentation on deserialization rules
- Proper escaping of serialized strings

### Changed

- Updated icons for `TomlImporter` and `TomlExporter` components

### Fixed

- Now properly ignores inline comments when parsing TOML
- Ignore escape sequences in literal strings

## [0.3.4] - 2022-12-28

### Added

- Serialization of jagged arrays
- Unit tests for jagged arrays

### Changed

- Made `HasDefaultConstructor` method private, as it is an internal helper method

### Fixed

- Escaping of quoted strings

## [0.3.3] - 2022-12-27

### Added

- `TomlImporter` component to import TOML files into Unity objects
- Custom editor for `TomlExporter` component
- Custom editor for `TomlImporter` component
- Sample scene for `TomlImporter` component
- Icons for `TomlExporter` and `TomlImporter` components

### Changed

- Renamed `sourceObject` to `targetObject` for `TomlExporter` component
- Removed `outputDirectory` from `TomlExporter` component
- Renamed `defaultFilename` to `outputFile` for `TomlExporter` component
- Updated documentation

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

- `TomlExporter` component for exporting TOML files from Unity objects
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

