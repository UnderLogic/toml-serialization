# TomlKeyAttribute

## Overview

The `TomlKeyAttribute` attribute is used to customize the name of a field when serializing to TOML.

It is also used when deserializing from TOML to map the TOML key to the field.

**NOTE:** The key can only contain alphanumeric characters, underscores (`_`), and hyphens (`-`).

## Limitations

The `TomlKeyAttribute` attribute can only be applied to fields.
This is because the attribute is used to customize the name of a field when serializing to TOML.

The key should not collide with any other keys in the same table, otherwise an exception will be thrown.
See [Limitations](../limitations.md) for more information.

## Public Properties

- `Key: string` **(get)** - The key to use when serializing and deserializing the field.

## Usage

### Example

```csharp
[Serializable]
public class Quest
{
    [TomlKey("quest_name")]
    private string _name;

    [TomlKey("quest_description")]
    private string _description;

    [TomlKey("quest_completed")]
    private bool _completed;
    
    [TomlKey("quest_completed_date")]
    private DateTime _completedDate;
}
```

Would serialize into...

```toml
quest_name = "Save the Princess"
quest_description = "The princess has been kidnapped by a dragon."
quest_completed = true
quest_completed_date = 2020-01-01T00:00:00Z
```

You can see that the `_name`, `_description`, `_completed`, and `_completedDate` fields are serialized and will appear in the output TOML.
They are serialized using the keys specified in the `TomlKeyAttribute` attribute.
