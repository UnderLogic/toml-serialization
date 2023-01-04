# ScriptableObject

## Overview

This library supports serializing and deserializing `ScriptableObject` fields.

These are serialized the same as a [Complex Type](complex-type.md).

Similarly, they must also be marked with the [`SerializableAttribute`](https://docs.microsoft.com/en-us/dotnet/api/system.serializableattribute).

## Serialization

`ScriptableObject` fields are serialized as a [TOML table](https://toml.io/en/v1.0.0#table), by default.

To override the default behavior, mark the field with the [`TomlInlineAttribute`](../attributes/toml-inline-attribute.md) or [`TomlExpandAttribute`](../attributes/toml-expand-attribute.md).

Use the [`NonSerializedAttribute`](https://docs.microsoft.com/en-us/dotnet/api/system.nonserializedattribute) to prevent a field from being serialized.

## Deserialization

`ScriptableObject` fields are deserialized from TOML as a [TOML table](https://toml.io/en/v1.0.0#table).

You must use the `DeserializeInto` method to deserialize into a `ScriptableObject`.
This is because the serializer cannot create a new instance of the `ScriptableObject` type.

You can manually create instances via [`ScriptableObject.CreateInstance`](https://docs.unity3d.com/ScriptReference/ScriptableObject.CreateInstance.html) and pass them to `DeserializeInto`.

## Example

```csharp
[Serializable]
public class PlayerCharacter : ScriptableObject
{
    private string _name;
    private int _level;
    private int _experience;
    private int _gold;
    private int _health;
    private int _mana;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
name = "Player"
level = 2
experience = 250
gold = 85
health = 100
mana = 50
```
