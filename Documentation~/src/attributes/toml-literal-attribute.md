# TomlLiteralAttribute

## Overview

The `TomlLiteralAttribute` attribute can be used to serialize a `field` as a literal string.
It acts as a hint to the serializer to write the field as a literal string, which will not escape any values.

It can be combined with the [`TomlMultilineAttribute`](toml-multiline-attribute.md) attribute to serialize a field as a literal multi-line string.

**NOTE:** This attribute has no effect on deserialization.

## Effect

- When applied to a `field` that would be serialized as a string, it will instead be serialized as a literal string using the single quote character (`'`).
- When applied to a `field` that would be serialized as a multi-line string, it will instead be serialized as a literal multi-line string using the triple single quote character (`'''`).
- When applied to a `field` that would be serialized as a string array, it will instead be serialized as a literal string array using the single quote character (`'`).

If the string value contains a single quote character, the string will be escaped with triple single quotes (`'''`).

## Usage

### Scalar Example

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
minLevel = 1
repeatable = true
```

Here the `_description` field has been serialized as a literal string, which means that the quotes around "Old Choppy" are not escaped.

### List Example

```csharp
[Serializable]
public class QuestLog
{
    [TomlLiteral]
    private List<string> _completedQuests;
}
```

Would serialize into...

```toml
completedQuests = [ 'Gathering Wood', 'The "Lost" Ring', '''Where's My Cow?''' ]
```

Here the `_completedQuests` field has been serialized as a literal array, which means that the quotes around "Lost" are not escaped.
The third element in the array is a literal multi-line string, which is serialized using triple single quotes (`'''`) to escape the single quote character.

This can be combined with the [`TomlMultilineAttribute`](toml-multiline-attribute.md) attribute to serialize the list as a multi-line array.

### Dictionary Example

```csharp
[Serializable]
public class Inventory
{
    [TomlLiteral]
    private Dictionary<string, string> _equipment;
}
```

Would serialize into...

```toml
equipment = { sword = 'The "Firey" Sword', shield = 'Aegis', helmet = '''Zeus' Helm''' }
```

Here the `_equipment` field has been serialized as a literal table, which means that the quotes around "Firey" are not escaped.
The third element in the table is a literal multi-line string, which is serialized using triple single quotes (`'''`) to escape the single quote character.
