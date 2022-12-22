# TomlSerializer

Static class that provides methods for deserializing TOML data into objects.

Also provides [Serialization](../serialization/toml-serializer.md) methods for serializing objects into TOML data.

## Description

Provides static methods for deserializing TOML into objects by reading from a string, stream, or text reader.

For more information on what can be deserialized, see the [Supported Types](../supported-types.md) page.

For more information on how objects are deserialized from TOML, see the [Deserialization Rules](../deserialization-rules.md) page.

## Public Methods

- `Deserialize<T>(string) : T` - Parses a TOML string into an object of type `T`.
- `Deserialize<T>(Stream, bool?) : T` - Parses a readable stream into an object of type `T`.
- `Deserialize<T>(TextReader) : T` - Parses a text reader into an object of type `T`.
- `DeserializeInto(string, object)` - Parses a TOML string into an existing object, overwriting existing values.
- `DeserializeInto(Stream, object, bool?)` - Parses a readable stream into an existing object, overwriting existing values.
- `DeserializeInto(TextReader, object)` - Parses a text reader into an existing object, overwriting existing values.

**NOTE:** The `Deserialize<T>` methods require `T` to be a class or struct that has a parameterless constructor.

**NOTE:** The `Deserialize` methods do not close the stream after reading from it.
To close the stream after reading from it, set the optional `leaveOpen` parameter to `false`.

## ScriptableObjects

The `TomlSerializer` class can be used to deserialize TOML data into a `ScriptableObject` instance using the `DeserializeInto` method.

```csharp
var obj = ScriptableObject.CreateInstance<MyScriptableObject>();
TomlSerializer.DeserializeInto(toml, obj);
```

**NOTE:** The `DeserializeInto` method will overwrite any existing values in the `ScriptableObject` instance, unless marked with the `NonSerialized` attribute.

## MonoBehaviours

The `TomlSerializer` class can be used to deserialize TOML data into a `MonoBehaviour` instance, using the `DeserializeInto` method.

**NOTE:** The `DeserializeInto` method will overwrite any existing values in the `MonoBehaviour` instance, unless marked with the `NonSerialized` attribute.
