# DateTime

## Overview

This library supports serializing and deserializing `DateTime` value fields.

## Serialization

DateTime values are serialized as a [TOML date-time](https://toml.io/en/v1.0.0#local-date-time).

They are serialized in ISO 8601 format, with the following format: `yyyy-MM-dd HH:mm:ss.FFFZ` by default.

The `Z` at the end indicates that the time is in UTC, and the `T` separator is omitted for clarity.

The format can be changed by specifying the [`TomlDateTimeFormatAttribute`](../attributes/toml-datetime-format-attribute.md).

## Deserialization

DateTime values are deserialized from TOML as a [TOML date-time](https://toml.io/en/v1.0.0#local-date-time).

Any format that is supported by the [`DateTime.Parse`](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.parse?view=net-7.0) method will be deserialized.

## Example

```csharp
[Serializable]
public class PlayerAccount
{
    private DateTime _createdDate;
    private DateTime _lastLoginDate;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
createdDate = 2021-01-01 00:00:00Z
lastLoginDate = 2022-08-04 00:00:00Z
```

