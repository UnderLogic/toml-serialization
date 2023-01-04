# Character

## Overview

This library supports serializing and deserializing `char` value fields.

## Serialization

Character values are serialized as a single character [TOML string](https://toml.io/en/v1.0.0#string).

## Deserialization

Character values are deserialized from TOML as a single character string.

If the string is empty, the character value is set to `'\0'`.

If the string is longer than one character, only the first character is used.

## Example

```csharp
[Serializable]
public class SystemLocale
{
    private char _thousandsSeparator;
    private char _decimalSeparator;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
thousandsSeparator = ","
decimalSeparator = "."
```
