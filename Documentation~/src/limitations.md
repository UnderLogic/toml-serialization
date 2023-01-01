# Limitations

## Overview

This package library is not intended to support every aspect of the TOML 1.0 specification.

Instead, it is intended to provide a simple, easy-to-use, and performant library for serializing and deserializing objects to and from TOML format for the majority of use cases.

With that, there are some limitations to the serialization process. These may be solved in future versions of the library.

## Field Name Collisions

When serializing an object to TOML, the library will attempt to use the field name as the key in the TOML document.
It will strip the leading underscore (`_`) from the field name if it exists and use the remaining name as the key.

If the object being serialized has two fields, one named `_name` and the other named `name`, the library will throw an exception.

### Example

```csharp
[Serializable]
public class SomeObject
{
    private string _name;
    private string name;  // <-- Will throw an exception on serialize
}
```

This is usually not a problem, as you should not have two fields with the same name in the same object.

### Workarounds

1. Rename one of the fields.
2. Use the [`TomlKeyAttribute`](attributes/toml-key-attribute.md) attribute to specify a different key name for the field.
3. Use the `NonSerializedAttribute` to exclude one of the fields from serialization.

## Key Names

When serializing an object the library will attempt to use the field name as the key in the TOML document.
For nested objects beyond the first level, the library will use dot (`.`) notation to represent the table name.

For key-value pairs, the key will never be serialized using dot notation.

The library does not attempt to escape the key names in any way.
This is usually not a problem, as C# field names usually are alphanumeric.

## Multiline Inline Tables

This library does not support multiline inline tables.
However, standard tables are supported as well as single-line inline tables.

### Example

```toml
# This is a valid standard table that spans multiple lines.
[table]
value = "bar"

# This is a valid single-line inline table.
inline_table = { foo = "bar", baz = "qux" }


# This is an invalid multiline inline table.
inline_table = {
    foo = "bar",
    baz = "qux"
}
```

The TOML specification highly recommends using standard tables over inline tables where possible.

## Interfaces

This library does not support two-way serialization of fields that represent interfaces.
This is because the library does not know which concrete type to use when deserializing the data.

Since fields can be made private you should not need to serialize interfaces.
Instead your backing fields should be of the concrete type, and the interface should be a property that can be accessed publicly.

## Abstract & Derived Classes

This library does not support two-way serialization of abstract or base classes.
This is because the library does not know which concrete type to use when deserializing the data.

When deserializing a base class, the library will only serialize the fields that are defined in the base class.
This can cause issues if the concrete type has additional fields that are not defined in the base class.

You should always use specific concrete types when serializing and deserializing data.

## Deeply Nested Objects

When serializing deeply nested objects, the library may incorrectly serialize the object or inline some of the fields.

While this is not a problem for most use cases, it is something to be aware of if you notice awkwardly formatted TOML documents.

## Multidimensional Arrays

This library does not support multidimensional arrays.
This is because it is not possible to represent multidimensional arrays in TOML.

However, jagged arrays ("arrays of arrays") are supported for scalar types.
They must be all of the same type, or boxed as `object` arrays.

For example, you can use `int[][]` but not `int[,]`.

## Unsigned 64-bit Integers

The `ulong` type is not supported by the TOML specification, so they are not supported by this library.
Unsigned 32-bit integers (`uint`) are supported, however.

You can use signed 64-bit integers (`long`) instead, as they are supported by the TOML specification.
All integer values are stored as signed 64-bit integers in the TOML document.

If you need large integer values, use `long` instead of `int` or `uint`.

## Floating Point Precision

When serializing floating point numbers, the library will use the default precision of the `double` type, which is 15 digits.
Any digits beyond the 15th will be rounded.

This is usually not a problem, but it is something to be aware of if you notice floating point numbers being rounded.

## Monetary Values

The `decimal` type is not supported. For storing monetary values, either use a `string` for the value or `int` for the value in cents.

Due to rounding errors, the `float` and `double` types are not recommended for storing monetary values.

### Example

```csharp
[Serializable]
public class InAppPurchase
{
    private int _priceInCents = 1299;
    private string _price = "12.99"
}
```
