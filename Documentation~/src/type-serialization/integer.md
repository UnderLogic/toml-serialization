# Integer

## Overview

This library supports serializing and deserializing the following integer value fields:

### Signed Types

- `sbyte` (signed 8-bit integer)
- `short` (signed 16-bit integer)
- `int` (signed 32-bit integer)
- `long` (signed 64-bit integer)

### Unsigned Types

- `byte` (unsigned 8-bit integer)
- `ushort` (unsigned 16-bit integer)
- `uint` (unsigned 32-bit integer)

**NOTE:** 64-bit unsigned integers (`ulong`) is not supported because TOML does not support them.

## Number Styles

By default, integer values are serialized in the decimal (base 10) format.

## Serialization

Integer values are serialized as a [TOML integer](https://toml.io/en/v1.0.0#integer).

The number format can be changed by specifying the [`TomlNumberFormatAttribute`](../attributes/number-format.md) on the field.

## Deserialization

Integer values are deserialized from TOML as a [TOML integer](https://toml.io/en/v1.0.0#integer).
Any number format is supported.

## Example

```csharp
[Serializable]
public class PlayerStats
{
    private int _health;
    private int _mana;
    private int _strength;
    private int _dexterity;
    private int _intelligence;
    private int _endurance;
    private int _luck;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
health = 100
mana = 25
strength = 9
dexterity = 7
intelligence = 3
endurance = 8
luck = 5
```
