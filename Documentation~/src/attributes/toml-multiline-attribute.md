# TomlMultilineAttribute

## Overview

The `TomlMultilineAttribute` attribute can be used to serialize a `field` as multi-line.

**NOTE:** This attribute has no effect on deserialization.

## Effect

- When applied to a `field` that would be serialized as a string, it will instead be serialized as a multi-line string using the triple double quote character (`"""`).
- When applied to a `field` that would be serialized as a literal string, it will instead be serialized as a literal multi-line string using the triple single quote character (`'''`).
- When applied to a `field` that would be serialized as an array or list, it will instead be serialized as a multi-line array with each element on a new line.

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
minLevel = 1
repeatable = true
quest_text = '''
Hello adventurer!
Are you interested in a quest?
I need you to gather 10 pieces of wood for me, "Old Choppy".

I'll pay you 10 gold for your troubles.'''
```

Notice that the `_questText` field has been serialized as a literal multi-line string, which means that the quotes around "Old Choppy" are not escaped.
All whitespace will be preserved after the first newline after the triple quotes.

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
aggroRadius = 10.0
dialogueOptions = [
    "Hello adventurer!",
    "Are you interested in a quest?",
    "Halt! You are not welcome here!",
]
```

Notice that the `_dialogueOptions` field has been serialized as a multi-line array, with each element on a new line.

Each item will be indent with 4 spaces, which is the default indentation for TOML.
