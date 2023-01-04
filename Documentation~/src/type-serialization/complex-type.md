# Complex Type

## Overview

This library supports serializing and deserializing complex type (`class` or `struct`) fields.

The same serialization rules apply to nested types as they do to the top-level type.

The complex type must be marked with the [`SerializableAttribute`](https://docs.microsoft.com/en-us/dotnet/api/system.serializableattribute).

## Parameterless Constructor

The serializer requires a parameterless constructor (`new()`) for the type.
This is because the serializer uses reflection to create an instance of the type to populate.

All `struct` types have a parameterless constructor, so they are supported.

You can use constructor chaining to create a parameterless constructor for a `class` type, initializing default values.

## Serialization

Complex type values are serialized as a [TOML table](https://toml.io/en/v1.0.0#table), by default.

To override the default behavior, mark the field with the [`TomlInlineAttribute`](../attributes/toml-inline-attribute.md) or [`TomlExpandAttribute`](../attributes/toml-expand-attribute.md).

## Deserialization

Complex type values are deserialized from TOML as a [TOML table](https://toml.io/en/v1.0.0#table).

## Example

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _experience;
    private int _gold;
    private int _health;
    private int _mana;
    private PlayerStats _stats;
    private int _statPoints;
}

[Serializable]
public class PlayerStats
{
    private int _stamina;
    private int _strength;
    private int _dexterity;
    private int _intelligence;
    private int _luck;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
name = "Player"
level = 3
experience = 1250
gold = 150
health = 75
mana = 125
statPoints = 2

[stats]
stamina = 2
strength = 4
dexterity = 6
intelligence = 8
luck = 5
```
