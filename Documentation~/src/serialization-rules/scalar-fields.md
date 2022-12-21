# Scalar Fields

## Overview

A scalar field is a field that is not an object, array, or dictionary.
It represents a single value, such as a `string`, `int`, or `DateTime`.

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
```

## Enum

Enum fields are mapped to a [TOML string](https://toml.io/en/v1.0.0#string) value and serialized as the result of calling the `ToString()` method on the enum value.

### Example

```toml
status = "Active"
directions = "North,East"
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
timestamp = 2020-01-01 00:00:00Z
```