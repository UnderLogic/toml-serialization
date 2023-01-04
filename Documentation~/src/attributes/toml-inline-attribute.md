# TomlInlineAttribute

## Overview

The `TomlInlineAttribute` attribute can be used to serialize a field as an inline table.
It acts as a hint to the serializer to write the dictionary of key-value pairs into an inline table.

It is the inverse of the [`TomlExpandAttribute`](toml-expand-attribute.md) attribute.

**NOTE:** This attribute has no effect on deserialization.

## Effect

When applied to a `field` that would be serialized as a standard table, it will instead be serialized as an inline table.

**NOTE:** This will cause child objects to be serialized as inline tables as well, as inline tables cannot contain standard tables.

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
minLevel = 1
repeatable = true
waypoints = { "start" = { x = 0, y = 0 }, "end" = { x = 10, y = 10 } }
```

Notice that the `_waypoints` field is serialized as an inline table instead of a standard table.
