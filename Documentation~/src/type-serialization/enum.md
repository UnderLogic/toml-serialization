# Enum

## Overview

This library supports serializing and deserializing `Enum` value fields.

## Bit Flags

Enums can be bitflags marked with the [`FlagsAttribute`](https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-6.0).

## Serialization

Enum values are serialized as a [TOML string](https://toml.io/en/v1.0.0#string).

If the enum is marked with the [`FlagsAttribute`](https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-6.0), the enum value is serialized as a comma-separated list of enum values.

## Deserialization

Enum values are deserialized from TOML as a [TOML string](https://toml.io/en/v1.0.0#string).

## Example

```csharp
[Serializable]
public class Hero
{
    private string _name;
    private int _level;
    private HeroClass _class;
    private StatusEffects _statusEffects;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
name = "Gandalf"
level = 100
class = "Wizard"
statusEffects = "Poisoned, Confused"
```
