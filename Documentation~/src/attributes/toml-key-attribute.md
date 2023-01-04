# TomlKeyAttribute

## Overview

The `TomlKeyAttribute` is used to customize the name of a field when serializing to TOML.

It is also used when deserializing from TOML to map the TOML key to the field.

**NOTE:** The key can only contain alphanumeric characters, underscores (`_`), and hyphens (`-`).

## Effect

When applied to a `field`, it will override the key of the field when serializing to TOML.

When deserializing from TOML, it will map the TOML key to the field.

**NOTE:** Two fields cannot have the same key within the same object.

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

Notice that the class fields have all been renamed to their corresponding TOML keys, prefixed with `quest_`.
