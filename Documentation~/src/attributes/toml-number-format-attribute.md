# TomlNumberFormatAttribute

## Overview

The `TomlNumberFormatAttribute` can be used to specify the format of an integer number field when serializing to TOML.

## Limitations

The `TomlNumberFormatAttribute` can only be applied to integer number fields.
It has no effect on floating point number fields.

## Number Formats

The following cases are supported via the `NumberFormat` enum value:

| Number Format | Decimal Value | Output String |
|---------------|---------------|---------------|
| Decimal       | `42`          | `42`          |
| HexLowerCase  | `42`          | `0x2a`        |
| HexUpperCase  | `42`          | `0x2A`        |
| Octal         | `42`          | `0o52`        |
| Binary        | `42`          | `0b101010`    |

The `Decimal` format is the default for all integer fields.

Other formats will always output their respective prefix (`0x`, `0o`, `0b`).

## Usage

### Example

```csharp
[Serializable]
public class FileInfo
{
    private string _name;
    private long _sizeInBytes;
    
    [TomlNumberFormat(NumberFormat.HexLowerCase)]
    private int _flags;
    
    [TomlNumberFormat(NumberFormat.Octal)]
    private int _permissions;
}
```

Would serialize into...

```toml
name = "file.txt"
size_in_bytes = 1024
flags = 0x2c04
permissions = 0o644
```

Here you can see that the `flags` and `permissions` fields are serialized in their respective formats.
