# TomlInlineAttribute

## Overview

The `TomlInlineAttribute` attribute can be used to serialize a field as an inline table.
It acts as a hint to the serializer to write the dictionary of key-value pairs into an inline table.

It is the inverse of the [`TomlExpandAttribute`](toml-expand-attribute.md) attribute.

This attribute has no effect on deserialization.

## Limitations

The `TomlInlineAttribute` attribute can only be applied to fields.

It has no effect when applied to fields that would not be serialized as a dictionary of key-value pairs.

It will force any nested tables to be serialized as inline tables.
This is because inline tables are not allowed to contain standard tables.

## Usage

### Example

```csharp
[Serializable]
public struct Waypoint
{
    private int _x;
    private int _y;
}

[Serializable]
public class Quest
{
    private string _name;
    private string _description;
    private int _minLevel;
    private bool _repeatable;
    
    [TomlInline]
    private Dictionary<string, Waypoint> _waypoints;
}
```

Would serialize into...

```toml
name = "Gather 10 Wood"
description = "Gather 10 pieces of wood."
min_level = 1
repeatable = true
waypoints = { "start" = { x = 0, y = 0 }, "end" = { x = 10, y = 10 } }
```

As you can see, the `_waypoints` field is serialized as an inline table instead of a standard table.
