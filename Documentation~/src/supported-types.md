# Supported Types

This package library aims to support the most common use cases for serializing and deserializing data.

To see more information how these are serialized, see the [Serialization Rules](serialization-rules.md) page.

## Scalar Types

The following "scalar" types are supported:

- `bool`
- `char`
- `string`
- `enum`
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

## Object Types

Custom `struct` and `class` objects are supported if they are marked with the `Serializable` attribute.

Unity's `ScriptableObject` and `MonoBehaviour` types are also supported.

**NOTE:** Each serialized field of the object must be one of the supported types, or marked with the `NonSerialized` attribute.

## Array Types

Array of supported types is also allowed, including `IEnumerable<T>` collections.

Custom objects that implement `IEnumerable<T>` are also supported if the `T` type is one of the supported types.

## Dictionary Types

Dictionaries are supported if `TKey` is `string` and `TValue` is one of the supported types.
This includes custom types, array types, and nested dictionary types as a value.

Custom objects that implement `IDictionary<string, TValue>` are also supported.
