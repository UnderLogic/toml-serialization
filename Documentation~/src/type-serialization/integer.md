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

Integer values are serialized as a [TOML integer](https://toml.io/en/v1.0.0#integer), in decimal (base 16) by default.

The number format can be changed by specifying one of the following attributes:

- [`TomlHexNumberAttribute`](../attributes/toml-hex-number-attribute.md) (base 16)
- [`TomlOctalNumberAttribute`](../attributes/toml-octal-number-attribute.md) (base 8)
- [`TomlBinaryNumberAttribute`](../attributes/toml-binary-number-attribute.md) (base 2)

## Deserialization

Integer values are deserialized from TOML as a [TOML integer](https://toml.io/en/v1.0.0#integer).

Underscores (`_`) can be used as digit separators for clarity.

Any valid number format is supported, including: decimal, hexadecimal, octal, and binary. These are specified with `0x`, `0o`, and `0b` prefixes.

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
