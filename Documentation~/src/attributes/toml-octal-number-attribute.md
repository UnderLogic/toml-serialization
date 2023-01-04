# TomlOctalNumberAttribute

## Overview

The `TomlOctalNumberAttribute` can be applied to any integer type `field` to serialize it as octal numbers.

**NOTE:** This attribute has no effect on deserialization, as the number format is inferred from the TOML document.

## Effect

When applied to a `field`, it will serialize the integer number as an octal number when serializing to TOML.

The value will be prefixed with `0o` when serialized.

## Usage

### Example

```csharp
[Serializable]
public class FileInfo
{
    private string _name;
    private long _sizeInBytes;
    
    [TomlOctalNumber]
    private int _permissions;
}
```

Would serialize into...

```toml
name = "file.txt"
sizeInBytes = 1024
permissions = 0o644
```

Notice that the `_permissions` field is serialized as an octal number.
