# Dictionary Fields

## Overview

A dictionary field is a field that is a `Dictionary<string, TValue>`, where `TValue` is a supported type.
`TValue` can be a scalar, object, array, list, or dictionary type.

## Definition

```csharp
[Serializable]
public class MyData
{
    private Dictionary<string, int> _scores;
    private Dictionary<string, string> _names;
    private Dictionary<string, object> _metadata;
}
```

## Mapping

A dictionary field is mapped to either an [inline TOML table](https://toml.io/en/v1.0.0#inline-table) or a [standard TOML table](https://toml.io/en/v1.0.0#table) based on the type of the dictionary values.

Mixed types are allowed, but will be boxed to `object` and serialized as their TOML representation.

### Scalar Example

If the dictionary contains scalar types, it is serialized as an [inline TOML table](https://toml.io/en/v1.0.0#inline-table).

You can override this behavior by using the [`TomlExpandAttribute`](../attributes/toml-expand-attribute.md) attribute to serialize the dictionary as a [standard TOML table](https://toml.io/en/v1.0.0#table).

```toml
scores = { math = 100, science = 90, english = 80 }
```

### Object Example

If the dictionary contains complex object types, it is serialized as a [standard TOML table](https://toml.io/en/v1.0.0#table).
Each key is a child table, using dot (`.`) notation to represent nested tables.

You can override this behavior by using the [`TomlInlineAttribute`](../attributes/toml-inline-attribute.md) attribute to serialize the dictionary as an [inline TOML table](https://toml.io/en/v1.0.0#inline-table).

```toml
[loot.common]
tableName = "common-loot"
chance = 0.25
rolls = 3

[loot.uncommon]
tableName = "uncommon-loot"
chance = 0.15
rolls = 2

[loot.rare]
tableName = "rare-loot"
chance = 0.05
rolls = 1
```

**NOTE:** Nested objects must be marked with the `Serializable` attribute.

### Array Example

If the dictionary contains array or list types, it is serialized as a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).
Each key is a child table, using dot (`.`) notation to represent nested table arrays.

These cannot be inlined.

```toml
[[weather.readings]]
weather = { temperature = 100, humidity = 50 }
timestamp = 2020-06-02 12:00:00.000Z

[[weather.readings]]
weather = { temperature = 90, humidity = 40 }
timestamp = 2020-06-01 12:00:00.000Z
```

**NOTE:** Array objects must be marked with the `Serializable` attribute.