# TomlBinaryNumberAttribute

## Overview

The `TomlBinaryNumberAttribute` can be applied to any integer type `field` to serialize them as binary numbers.

**NOTE:** This attribute has no effect on deserialization, as the number format is inferred from the TOML document.

## Effect

When applied to a `field`, it will serialize the integer number as a binary number when serializing to TOML.

The value will be prefixed with `0b` when serialized.

## Usage

### Example

```csharp
[Serializable]
public class FileInfo
{
    private string _name;
    private long _sizeInBytes;
    
    [TomlBinaryNumber]
    private int _flags;
}
```

Would serialize into...

```toml
name = "file.txt"
sizeInBytes = 1024
flags = 0b10110100
```

Notice that the `_flags` field is serialized as a binary number.
