# TomlSerializer

Static class that provides methods for serializing and deserializing objects to and from TOML format.

## Description

Provides static methods for serializing objects to and from TOML via `string`, `Stream`, `TextReader` or `TextWriter` instances.

For more information on what can be serialized, see the [Supported Types](../supported-types.md) page.

For more information on how objects are serialized to TOML, see the [Serialization Rules](../serialization-rules.md) page.

**NOTE**: There are [limitations](../limitations.md) to the serialization process.

## Public Methods

- `Serialize(object) : string` - Returns a string containing the TOML representation of the serialized object.
- `Serialize(object, Stream, bool?)` - Writes the TOML representation of the serialized object to the specified stream.
- `Serialize(object, TextWriter)` - Writes the TOML representation of the serialized object using the specified text writer.


- `Deserialize<T>(string) : T` - Returns an object of type `T` deserialized from the TOML string.
- `Deserialize<T>(Stream, bool?) : T` - Returns an object of type `T` deserialized from the TOML stream.
- `Deserialize<T>(TextReader) : T` - Returns an object of type `T` deserialized from the TOML text reader.
- `DeserializeInto(string, object)` - Deserializes the TOML string into the specified object, overwriting any existing values.
- `DeserializeInto(Stream, object, bool?)` - Deserializes the TOML stream into the specified object, overwriting any existing values.
- `DeserializeInto(TextReader, object)` - Deserializes the TOML text reader into the specified object, overwriting any existing values.

**NOTE:** The `Stream` methods do not close the stream after writing to it.
To close the stream after writing to it, set the optional `leaveOpen` parameter to `false`.

## Serialization

The `TomlSerializer` class can be used to serialize objects to TOML.

The object being serialized **must** be decorated with the [`SerializableAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.serializableattribute?view=net-6.0).
Any child fields that are object types must also be decorated with the [`SerializableAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.serializableattribute?view=net-6.0).

### Example

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _health;
    private int _maxHealth;
    private int _gold;
}

// Somewhere else in your code...

public void SavePlayerCharacter(PlayerCharacter playerCharacter)
{
    var directory = UnityEngine.Application.persistentDataPath;
    var saveFile = Path.Combine(directory, "save.toml");
    
    using (var stream = File.OpenWrite(saveFile))
    {
        TomlSerializer.Serialize(playerCharacter, stream);
    }
}
```

After running the above code, the `save.toml` file will contain the following:

```toml
name = "Player 1"
level = 7
health = 160
maxHealth = 200
gold = 1250
```

## Deserialization

The `TomlSerializer` class can be used to deserialize objects from TOML.

The object being deserialized **must** be decorated with the [`SerializableAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.serializableattribute?view=net-6.0).
Any child fields that are object types must also be decorated with the [`SerializableAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.serializableattribute?view=net-6.0).

**NOTE:** When using the `Deserialize<T>` method, the `T` type parameter must have a parameterless constructor.

### Example

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _health;
    private int _maxHealth;
    private int _gold;
}

// Somewhere else in your code...

public void LoadPlayerCharacter()
{
    var directory = UnityEngine.Application.persistentDataPath;
    var fileToLoad = Path.Combine(directory, "save.toml");
    
    using (var stream = File.OpenRead(fileToLoad))
    {
        var playerCharacter = TomlSerializer.Deserialize<PlayerCharacter>(stream);
        // or...
        var playerCharacter = new PlayerCharacter();
        TomlSerializer.DeserializeInto(stream, playerCharacter);
    }
}
```