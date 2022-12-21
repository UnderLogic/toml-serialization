# TomlSerializer

Static class that provides methods for serializing objects to TOML format.

## Description

Provides static methods for serializing objects into TOML and writing them to a string, stream, or text writer.

For more information on what can be serialized, see the [Supported Types](../supported-types.md) page.

For more information on how objects are serialized to TOML, see the [Serialization Rules](../serialization-rules.md) page.

## Public Methods

- `Serialize(object) : string` - Returns a string containing the TOML representation of the serialized object.
- `Serialize(object, Stream, bool?)` - Writes the TOML representation of the serialized object to the specified stream.
- `Serialize(object, TextWriter)` - Writes the TOML representation of the serialized object using the specified text writer.

**NOTE:** The `Serialize(object, Stream, bool?)` method does not close the stream after writing to it.
To close the stream after writing to it, set the optional `leaveOpen` parameter to `false`.
