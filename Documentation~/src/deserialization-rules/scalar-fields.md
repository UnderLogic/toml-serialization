# Scalar Fields

## Overview

A scalar field is a field that is not a complex object or collection.
It represents a single value, such as a `string`, `int`, `Enum`, or `DateTime`.

It can also represent a boxed `object` value that contains a scalar value.

## Mapping

A scalar field is mapped to a TOML value based on its type.
For more information on what types are supported, see the [Supported Types](../supported-types.md) page.

## Coercion

When deserializing a TOML value to a scalar field, the TOML value is coerced to the type of the field.
This may have unexpected results if the TOML value is not compatible with the field type.

If the TOML value is not compatible with the field type, an exception is thrown.
No additional parsing or coercion is performed outside of the standard C# type conversion rules.

## Boolean

Boolean fields are deserialized from a [TOML boolean](https://toml.io/en/v1.0.0#boolean) value and coerced to a `bool` value.
Only `true` or `false` are valid values.

### Example

```toml
enabled = true
is_admin = false
```

## Character

Character fields are deserialized from a [TOML string](https://toml.io/en/v1.0.0#string) value and coerced to a `char` value.
Only the first character of the string is used.

**NOTE:** Empty strings are not valid values, and will throw an exception.

### Example

```toml
letter = "A"
number = "9"
symbol = "$"
```

## String

String fields are deserialized from a [TOML string](https://toml.io/en/v1.0.0#string) value and coerced to a `string` value.
Both basic and literal strings are supported, as well as multi-line strings of each.

You can also use unicode escape sequences in strings with the `\uXXXX` format.

**NOTE:** Literal strings are not escaped, and will contain the literal characters in the string.

### Basic Example

```toml
message = "Hello, world!"
quoted = "This is a \"quoted\" string."
path = "C:\\Users\\username\\Documents"
drive = "\\\\server\\\\share\\\\folder"
with_whitespace = "Each\\nline\\nshould\\nbe\\nseparated\\nby\\na\\nnewline\\ncharacter."
unicode = "Unicode escape sequences are supported: \\u00A9"
```

### Literal Example

```toml
message = 'Hello, world!'
quoted = 'This is a "quoted\" string.'
path = 'C:\Users\username\Documents'
drive = '\\server\\share\\folder'
```

### Basic Multiline Example

```toml
summary = """
This is a summary of the document.
It contains multiple lines of text.
"""
quoted = """This is a single line with "quotes" inside, or even doubly quoted ""strings"" inside."""
trimmed = """\
  You can use this to \
     trim the whitespace between lines \
        without worrying about newlines."""
```

### Literal Multiline Example

```toml
summary = '''
This is a summary of the document.
It contains multiple lines of text.
'''
quoted = '''This is a single line with "quotes" inside, or even doubly quoted ""strings"" inside.'''
verbatim = '''
In here you can have "quotes" and paths like C:\Users\username\Documents or \\server\share\folder.
Nothing is escaped in here.
'''
```

## Enum

Enum fields are deserialized from a [TOML string](https://toml.io/en/v1.0.0#string) value and parsed to an `Enum` value.
The string value must match the name of one of the enum values.

Bit flags using the `FlagsAttribute` are also supported. Multiple values can be specified by separating them with a comma.

### Example

```toml
status = "Active"
directions = "North, East"
```

## Integer

Integer fields are deserialized from a [TOML integer](https://toml.io/en/v1.0.0#integer) value and coerced to one of the following values:

### Signed Types

- `sbyte` - signed 8-bit integer
- `short` - signed 16-bit integer
- `int` - signed 32-bit integer
- `long` - signed 64-bit integer

### Unsigned Types

- `byte` - unsigned 8-bit integer
- `ushort` - unsigned 16-bit integer
- `uint` - unsigned 32-bit integer

Any fractional part of the value is discarded.

**NOTE:** Unsigned 64-bit integers (`ulong`) are not supported by TOML.

### Number Formats

The following number formats are allowed:

- Base 10 (`dec`)
- Base 16 (`hex`)
- Base 8 (`oct`)
- Base 2 (`bin`)

### Example

```toml
positive = 42
negative = -42
flags = 0x1CFF
file_permissions = 0o755
bin_value = 0b1101010
```

### Digit Separators

The underscore (`_`) character can be used as a digit separator in numeric values.
It is ignored when parsing the value.

#### Example

```toml
positive = 1_000_000
negative = -1_000_000
flags = 0xdead_beef
```

## Floating Point

Floating point fields are deserialized from a [TOML float](https://toml.io/en/v1.0.0#float) value and coerced to one of the following values:

- `float` - 32-bit floating point number
- `double` - 64-bit floating point number

### Example

```toml
pi = 3.14
multiplier = -1.0e6
```

**NOTE:** Scientific notation is supported.

### Special Values

The following special values are supported:

- `+inf` - positive infinity
- `-inf` - negative infinity
- `+nan` - positive not-a-number
- `-nan` - negative not-a-number

## DateTime

DateTime fields are deserialized from a [TOML datetime](https://toml.io/en/v1.0.0#local-date-time) value and parsed as a `DateTime` value.
The date string must be in a format that can be parsed by `DateTime.Parse`.

It is recommended to use the [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) format.

### Example

```toml
timestamp = 2020-01-01 12:34:56.789Z
```

**NOTE:** DateTime values are **not** enclosed in double quotes. This is a TOML requirement to distinguish between a date and time and a string.

## Object

Boxed `object` fields are mapped to their underlying type and deserialized based on the native underlying type.

|   Toml Value   | Unboxed Type |
|:--------------:|:------------:|
|   `TomlNull`   |    `null`    |
|   `TomlBool`   |    `bool`    |
|  `TomlString`  |   `string`   |
| `TomlInteger`  |    `long`    |
|  `TomlFloat`   |   `double`   |
| `TomlDateTime` |  `DateTime`  |
|  `TomlArray`   |  `object[]`  |

This is useful when used in conjunction with arrays, lists, and dictionaries for mixed collections.
