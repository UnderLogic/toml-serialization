# TomlLiteralAttribute

## Overview

The `TomlLiteralAttribute` attribute can be used to serialize a field as a literal string.
It acts as a hint to the serializer to write the field as a literal string, which will not escape any values.

It can be combined with the [`TomlMultilineAttribute`](toml-multiline-attribute.md) attribute to serialize a field as a literal multi-line string.

**NOTE:** If the string contains a single quote character, it will be serialized as a multi-line literal string using triple single quotes (`'''`).

## Limitations

The `TomlLiteralAttribute` attribute can only be applied to fields.

It has no effect when applied to fields that would not be serialized as a string.

## Usage

### Example

```csharp
[Serializable]
public class Quest
{
    private string _name;
    
    [TomlLiteral] 
    private string _description;
    
    private int _minLevel;
    private bool _repeatable;
}
```

Would serialize into...

```toml
name = "Gather 10 Wood"
description = 'Gather 10 pieces of wood for "Old Choppy".'
min_level = 1
repeatable = true
```

Here the `_description` field has been serialized as a literal string, which means that the quotes around "Old Choppy" are not escaped.
