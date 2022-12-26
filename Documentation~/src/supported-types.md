# Supported Types

This package library aims to support the most common use cases for serializing and deserializing data.

To see more information how these are serialized into TOML, see the [Serialization Rules](serialization-rules.md) page.

## Scalars

The following scalar types are supported:

- `bool`
- `char`
- `string`
- `sbyte`
- `short`
- `int`
- `long`
- `byte`
- `ushort`
- `uint`
- `float`
- `double`
- `DateTime`

**NOTE:** Unsigned 64-bit integer types (`ulong`) are not supported, per the [TOML specification](https://toml.io/en/v1.0.0#integer).

## Enum Values

Enum values are supported including bitflags marked with the `Flags` attribute.

## Object Values

Boxed `object` values are supported.
This can be useful when used in conjunction with arrays, lists, and dictionaries for mixed collections.

## Structs

Custom struct types are supported and must be marked with the `Serializable` attribute.

## Classes

Custom class types are supported and must be marked with the `Serializable` attribute.

Unity's `ScriptableObject` and `MonoBehaviour` types are supported, but must also be marked with the `Serializable` attribute.

## Arrays

Arrays of any supported type are allowed and can be serialized.

## Lists

Lists of any supported type are allowed and can be serialized.
This includes `IList` non-generic and `IList<T>` generic interfaces.

## Dictionaries

Dictionaries are supported if `TKey` is `string` and `TValue` is one of the supported types.
This includes custom types, array types, and nested dictionary types as a value.
