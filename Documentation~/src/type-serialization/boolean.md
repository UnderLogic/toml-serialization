# Boolean

## Overview

This library supports serializing and deserializing `bool` value fields.

## Serialization

Boolean values are serialized as either `true` or `false` as a [TOML boolean](https://toml.io/en/v1.0.0#boolean).
These are always lowercase.

## Deserialization

Boolean values are deserialized from TOML as either `true` or `false`, case-insensitive.

## Example

```csharp
[Serializable]
public class PlayerAccount
{
    private bool _isPremium;
    private bool _isBanned;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
isPremium = true
isBanned = false
```
