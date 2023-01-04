# Dictionary

## Overview

This library supports serializing and deserializing `Dictionary<string, TValue>` fields, where `TValue` is a supported type.

## Mixed Dictionaries

Dictionary values can contain mixed types as long as the dictionary is defined as `Dictionary<string, object>`.

Each value in the dictionary will be serialized as its own TOML value.

## Serialization

Dictionary values are serialized differently based on their value type.

- Scalar types are serialized as an [inline TOML table](https://toml.io/en/v1.0.0#inline-table), by default.
- Mixed types are serialized as an [inline TOML table](https://toml.io/en/v1.0.0#inline-table), by default.
- Complex types are serialized as a [standard TOML table](https://toml.io/en/v1.0.0#table), by default.

To override the default behavior, mark the field with the [`TomlInlineAttribute`](../attributes/toml-inline-attribute.md) or [`TomlExpandAttribute`](../attributes/toml-expand-attribute.md).

## Deserialization

Dictionary values are deserialized from TOML as an [inline TOML table](https://toml.io/en/v1.0.0#inline-table) or [standard TOML table](https://toml.io/en/v1.0.0#table).

Mixed dictionaries are supported, but must be defined as `Dictionary<string, object>`.

## Example

```csharp
[Serializable]
public class PlayerEquipment
{
    private Dictionary<string, string> _tags;
    private Dictionary<string, Item> _gear;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
tags = { loot = "common" }

[gear.weapon]
name = "Sword"
weight = 5.1

[gear.shield]
name = "Shield"
weight = 10.2
```

