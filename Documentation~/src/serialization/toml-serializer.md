# TomlSerializer

Static class that provides methods for serializing objects to TOML format.

Also provides [Deserialization](../deserialization/toml-serializer.md) methods for deserializing objects from TOML data.

## Description

Provides static methods for serializing objects into TOML and writing them to a string, stream, or text writer.

For more information on what can be serialized, see the [Supported Types](../supported-types.md) page.

For more information on how objects are serialized to TOML, see the [Serialization Rules](../serialization-rules.md) page.

Please note the [Limitations](../limitations.md) of the serialization process.

## Public Methods

- `Serialize(object) : string` - Returns a string containing the TOML representation of the serialized object.
- `Serialize(object, Stream, bool?)` - Writes the TOML representation of the serialized object to the specified stream.
- `Serialize(object, TextWriter)` - Writes the TOML representation of the serialized object using the specified text writer.

**NOTE:** The `Serialize` method does not close the stream after writing to it.
To close the stream after writing to it, set the optional `leaveOpen` parameter to `false`.

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
