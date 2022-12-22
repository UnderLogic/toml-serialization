# Limitations

## Overview

This package library is not intended to support every aspect of the TOML 1.0 specification.

Instead, it is intended to provide a simple, easy-to-use, and performant library for serializing and deserializing objects to and from TOML format for the majority of use cases.

With that, there are some limitations to the serialization process. These may be solved in future versions of the library.

## Field Name Collisions

When serializing an object to TOML, the library will attempt to use the field name as the key in the TOML document.
It will strip the leading underscore (`_`) from the field name if it exists and use the remaining name as the key.

If the object being serialized has two fields, one named `_name` and the other named `name`, the library will throw an exception.

This is usually not a problem, as you should not have two fields with the same name in the same object.

## Key Names

When serializing an object the library will attempt to use the field name as the key in the TOML document.
For nested objects beyond the first level, the library will use dot (`.`) notation to represent the table name.

For key-value pairs, the key will never be serialized using dot notation.

The library does not attempt to escape the key names in any way.
This is usually not a problem, as C# field names usually are alphanumeric.

## Deeply Nested Objects

When serializing deeply nested objects, the library may incorrectly serialize the object or inline some of the fields.

While this is not a problem for most use cases, it is something to be aware of if you notice awkwardly formatted TOML documents.

## Multidimensional Arrays

This library does not currently support multidimensional or jagged arrays, but it may be added in a future version.
As a workaround you can use a custom type that represents a complex array and implement `IEnumerable`, `ICollection` or `IList` interfaces.

## Unsigned 64-bit Integers

These are not supported by the TOML specification, so they are not supported by this library.
Unsigned 32-bit integers are supported, however.

You can use signed 64-bit integers instead, as they are supported by the TOML specification.

## Floating Point Precision

When serializing floating point numbers, the library will use the default precision of the `double` type, which is 15 digits.
Any digits beyond the 15th will be rounded.

This is usually not a problem, but it is something to be aware of if you notice floating point numbers being rounded.