# List

## Overview

This library supports serializing and deserializing `List<T>` value fields.

Any type that is supported can be serialized and deserialized as a list of that type.

## Mixed Lists

List can contain mixed types as long as the list is defined as `List<object>`.

Each element in the list will be serialized as its own TOML value.

## Serialization

List values are serialized differently based on their element type.

- Scalar types are serialized as a [TOML array](https://toml.io/en/v1.0.0#array).
- Mixed types are serialized as a [TOML array](https://toml.io/en/v1.0.0#array).
- Complex types are serialized as a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).

By default, TOML arrays are serialized as inline arrays. This can be changed by marking the field with the [`TomlMultilineAttribute`](../attributes/toml-multiline-attribute.md).

## Deserialization

List values are deserialized from TOML as a [TOML array](https://toml.io/en/v1.0.0#array) or [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).

Mixed lists are supported, but must be defined as `List<object>`.

## Example

```csharp
[Serializable]
public class PlayerInventory
{
    private List<string> tags;
    private List<Item> _items;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
tags = ["loot", "common"]

[[items]]
name = "Sword"
weight = 5.1

[[items]]
name = "Shield"
weight = 10.2
```
