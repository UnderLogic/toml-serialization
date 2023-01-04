# TomlHexNumberAttribute

## Overview

The `TomlHexNumberAttribute` attribute can be applied to any integer type `field` to serialize them as hexadecimal numbers.

**NOTE:** This attribute has no effect on deserialization, as the number format is inferred from the TOML document.

## Effect

When applied to a `field`, it will serialize the integer number as a hexadecimal number when serializing to TOML.

By default, it will be serialized as a lowercase hexadecimal number. This can be changed by passing `true` in the constructor.

The value will be prefixed with `0x` when serialized.

## Public Properties

- `IsUpperCase : bool` **(get)** - Whether the hexadecimal number should be serialized as an uppercase number.

## Usage

### Example

```csharp
[Serializable]
public class FileInfo
{
    private string _name;
    private long _sizeInBytes;
    
    [TomlHexNumber]
    private int _flags;
}
```

Would serialize into...

```toml
name = "file.txt"
size_in_bytes = 1024
flags = 0x2c04
```

Notice that the `_flags` field is serialized as a hex number (lowercase).
