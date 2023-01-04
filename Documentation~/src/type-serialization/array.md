# Array

## Overview

This library supports serializing and deserializing `Array` value fields.

Any type that is supported can be serialized and deserialized as an array of that type.

**NOTE:** It is recommended to use [`List`](list.md) fields instead of `Array` fields, when possible.

## Mixed Arrays

Arrays can contain mixed types as long as the array is defined as `object[]`.

Each element in the array will be serialized as its own TOML value.

## Jagged Arrays

Multidimensional arrays (ex: `int[,]`) are not supported.

Instead, use jagged arrays (ex: `int[][]`).

## Serialization

Array values are serialized differently based on their element type.

- Scalar types are serialized as a [TOML array](https://toml.io/en/v1.0.0#array).
- Mixed types are serialized as a [TOML array](https://toml.io/en/v1.0.0#array).
- Complex types are serialized as a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).
- Jagged arrays serialized as a [TOML array of arrays](https://toml.io/en/v1.0.0#array-of-arrays).

By default, TOML arrays are serialized as inline arrays. This can be changed by marking the field with the [`TomlMultilineAttribute`](../attributes/toml-multiline-attribute.md).

## Deserialization

Array values are deserialized from TOML as a [TOML array](https://toml.io/en/v1.0.0#array) or [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).

Mixed arrays are supported, but must be defined as `object[]`.

## Example

```csharp
[Serializable]
public class PlayerInventory
{
    private string[] tags;
    private Item[] _items;
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
