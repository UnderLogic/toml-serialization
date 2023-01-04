# TomlDateTimeFormatAttribute

## Overview

The `TomlDateTimeFormatAttribute` can be applied to any `DateTime` `field` to serialize them as a specific date-time format.

**NOTE:** This attribute has no effect on deserialization, as the date-time format is inferred from the TOML document.

## Effect

When applied to a `field`, it will serialize the `DateTime` as a specific date-time format when serializing to TOML.

The value will be serialized according to the specified format string.

## Public Properties

- `DateTimeFormat : string` **(get)** - The format string to use when serializing the `DateTime`.

## Usage

### Example

```csharp
[Serializable]
public class FileInfo
{
    private string _name;
    private long _sizeInBytes;
    
    [TomlDateTimeFormat("yyyy-MM-dd")]
    private DateTime _lastModified;
}
```

Would serialize into...

```toml
name = "file.txt"
sizeInBytes = 1024
lastModified = 2020-01-01
```

Notice that the `_lastModified` field is serialized as a specific date-time format.
