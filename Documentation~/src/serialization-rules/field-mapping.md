# Field Mapping

## Overview

This library uses reflection to determine the name of each field when deserializing TOML into an object.

By default, the name of each field is used as the key in the TOML document.
This can be overridden by using the [`TomlKeyAttribute`](attributes/toml-key-attribute.md) or [`TomlCasingAttribute`](attributes/toml-casing-attribute.md).

Any leading underscores (`_`) are removed from the field name before it is used as the key in the TOML document, unless explicitly named.

### Example

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _experience;
}
```

This object can be deserialized from the following TOML document:

```toml
name = "Hero"
level = 7
experience = 1250
```

Notice that the leading underscore (`_`) in the field name is ignored when deserializing the TOML document into the object.
