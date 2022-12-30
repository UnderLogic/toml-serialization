# Scalar Fields

## Overview

A scalar field is a field that is not a complex object or collection.
It represents a single value, such as a `string`, `int`, `Enum`, or `DateTime`.

It can also represent a boxed `object` value that contains a scalar value.

## Mapping

A scalar field is mapped to a TOML value based on its type.
For more information on what types are supported, see the [Supported Types](../supported-types.md) page.

## Boolean

Boolean fields are mapped to a [TOML boolean](https://toml.io/en/v1.0.0#boolean) value and serialized as `true` or `false`.

### Example

```toml
enabled = true
```

## Character

Character fields are mapped to a [TOML string](https://toml.io/en/v1.0.0#string) value and serialized as a single character, surrounded by double quotes.

### Example

```toml
letter = "A"
number = "9"
symbol = "$"
```

## String

String fields are mapped to a [TOML string](https://toml.io/en/v1.0.0#string) value and serialized as a sequence of characters, surrounded by double quotes.

### Example

```toml
message = "Hello, world!"
quoted = "This is a \"quoted\" string."
path = "C:\\Users\\username\\Documents"
```

**NOTE:** The TOML specification requires that double quotes and backslashes be escaped with a backslash.

## Enum

Enum fields are mapped to a [TOML string](https://toml.io/en/v1.0.0#string) value and serialized as the result of calling the `ToString()` method on the enum value.

### Example

```toml
status = "Active"
directions = "North, East"
```

**NOTE:** Bit flags will be serialized as a comma-separated list of the individual flags.

## Integer

Integer fields are mapped to a [TOML integer](https://toml.io/en/v1.0.0#integer) value and serialized as a sequence of digits.

### Example

```toml
positive = 42
negative = -42
```

**NOTE:** Unsigned 64-bit integers are not supported by TOML.

## Floating Point

Floating point fields are mapped to a [TOML float](https://toml.io/en/v1.0.0#float) value and serialized as a sequence of digits, with an optional decimal point.

### Example

```toml
pi = 3.14
multiplier = -1.0e6
```

**NOTE:** Scientific notation is supported.

## DateTime

DateTime fields are mapped to a [TOML datetime](https://toml.io/en/v1.0.0#local-date-time) value and serialized as a date and time.
By default, the date and time are serialized in UTC and using the ISO 8601 format.

### Example

```toml
timestamp = 2020-01-01 12:34:56.789Z
```

## Object

Boxed `object` fields are mapped to their underlying type and serialized based on the type of the boxed value.

This is useful when used in conjunction with arrays, lists, and dictionaries for mixed collections.
