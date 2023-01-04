# Floating Point

## Overview

This library supports serializing and deserializing `float` and `double` value fields.

## Special Values

The following special values are supported:

- `NaN` (not a number) as `nan` or `-nan`
- `PositiveInfinity` as `inf` or `+inf`
- `NegativeInfinity` as `-inf`

## Serialization

Floating point values are serialized as a [TOML float](https://toml.io/en/v1.0.0#float).

## Deserialization

Floating point values are deserialized from TOML as a [TOML float](https://toml.io/en/v1.0.0#float).

Scientific notation is supported using the `e` character.

Any of the special values are supported.

## Example

```csharp
[Serializable]
public class PlayerStats
{
    private float _health;
    private float _maxHealth;
    private float _mana;
    private float _maxMana;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
health = 50.0
maxHealth = 100.0
mana = 20.0
maxMana = 40.0
```
