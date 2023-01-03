# Boolean

## Overview

This library supports serializing and deserializing `bool` value fields.

## Serialization

Boolean values are serialized as either `true` or `false` in TOML.
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
