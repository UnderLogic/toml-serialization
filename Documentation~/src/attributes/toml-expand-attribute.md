# TomlExpandAttribute

## Overview

The `TomlExpandAttribute` attribute can be used to serialize a field as a standard table.
It acts as a hint to the serializer to expand the dictionary of key-value pairs into a standard table.

It acts as the inverse of the [`TomlInlineAttribute`](toml-inline-attribute.md) attribute.

## Limitations

The `TomlExpandAttribute` attribute can only be applied to fields.

It has no effect when applied to fields that would not be serialized as a dictionary of key-value pairs.

Also, it will be ignored when the field is nested inside an inlined table.
This is because inlined tables are not allowed to contain standard tables.

## Usage

### Example

```csharp
[Serializable]
public class Quest
{
    private string _name;
    private string _description;
    private int _minLevel;
    private bool _repeatable;
    
    [TomlExpand]
    private Dictionary<string, int> _rewards;
}
```

Would serialize into...

```toml
name = "Gather 10 Wood"
description = "Gather 10 pieces of wood."
min_level = 1
repeatable = true

[rewards]
experience = 100
gold = 50
oak_stick = 1
apples = 5
```

As you can see, the `_rewards` field is serialized as a standard table instead of an inline table.
