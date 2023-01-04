# TomlExpandAttribute

## Overview

The `TomlExpandAttribute` attribute can be used to serialize a field as a standard table.
It acts as a hint to the serializer to expand the dictionary of key-value pairs into a standard table instead of an inline table.

It is the inverse of the [`TomlInlineAttribute`](toml-inline-attribute.md) attribute.

**NOTE:** This attribute has no effect on deserialization.

## Effect

When applied to a `field` that would be serialized as an inline table, it will instead be serialized as a standard table.

**NOTE:** This will be ignored when the parent object is serialized as an inline table, as inline tables cannot contain standard tables.

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
minLevel = 1
repeatable = true

[rewards]
experience = 100
gold = 50
oakStick = 1
apples = 5
```

Notice that the `_rewards` field is serialized as a standard table instead of an inline table.
