# TomlMultilineAttribute

## Overview

The `TomlMultilineAttribute` attribute can be used to serialize a field as a multi-line string.
It acts as a hint to the serializer to write the field as a multi-line string, which will escape any values while retaining whitespace.

It can be combined with the [`TomlLiteralAttribute`](toml-literal-attribute.md) attribute to serialize a field as a literal multi-line string.

This attribute has no effect on deserialization.

## Limitations

The `TomlMultilineAttribute` attribute can only be applied to fields.

It has no effect when applied to fields that would not be serialized as a string.
In the future, it may be extended to support other types (such as arrays and lists).

## Usage

### Example

```csharp
[Serializable]
public class Quest
{
    private string _name;

    private string _description;
    private int _minLevel;
    private bool _repeatable;
    
    [TomlLiteral]
    [TomlMultiline]
    private string _questText;
}
```

Would serialize into...

```toml
name = "Gather 10 Wood"
description = "Gather 10 pieces of wood."
min_level = 1
repeatable = true
quest_text = '''
Hello adventurer!
Are you interested in a quest?
I need you to gather 10 pieces of wood for me, "Old Choppy".

I'll pay you 10 gold for your troubles.'''
```

Here the `_questText` field has been serialized as a literal multi-line string, which means that the quotes around "Old Choppy" are not escaped.
All whitespace is also preserved for the multi-line string.
