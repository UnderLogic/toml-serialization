# TomlMultilineAttribute

## Overview

The `TomlMultilineAttribute` attribute can be used to serialize a field as multi-line.
For strings, it acts as a hint to the serializer to write the field as a multi-line string.
For arrays, it acts as a hint to the serializer to write the array as a multi-line array.

It can be combined with the [`TomlLiteralAttribute`](toml-literal-attribute.md) attribute to serialize a field as a literal multi-line string.

This attribute has no effect on deserialization.

## Limitations

The `TomlMultilineAttribute` attribute can only be applied to fields.

It has no effect when applied to fields that would not be serialized as a string or collection of strings (arrays, lists, dictionaries).

## Usage

### String Example

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

### Array Example

```csharp
[Serializable]
public class Guardian
{
    private string _name;
    private float _aggroRadius;
    
    [TomlMultiline]
    private List<string> _dialogueOptions;
}
```

Would serialize into...

```toml
name = "Guardian"
aggro_radius = 10.0
dialogueOptions = [
    "Hello adventurer!",
    "Are you interested in a quest?",
    "Halt! You are not welcome here!",
]
```

Here the `_dialogueOptions` field has been serialized as a multi-line array, which means that the array is written on multiple lines.
It will include the trailing comma on the last element, which is valid TOML.

Each item will be indent with 4 spaces, which is the default indentation for TOML.
