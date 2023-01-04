# String

## Overview

This library supports serializing and deserializing `string` value fields.

## Literal Strings

Literal strings are serialized using the single quote (`'`) character, with no escaping of special characters.

For literal strings containing single quotes, the string must be enclosed in triple quotes (`'''`).

## Multiline Strings

Multiline strings are serialized using the triple quote (`"""`) character.

Literal multiline strings are serialized using the triple single quote (`'''`) character.

## Escaped Strings

Basic strings must escape the following characters:

- `\\` (backslash)
- `\"` (double quote)
- `\n` (newline)
- `\r` (carriage return)
- `\t` (tab)
- `\uXXXX` (unicode character)

## Serialization

String values are serialized as [TOML basic strings](https://toml.io/en/v1.0.0#string) by default.

Strings can be serialized as literal strings in TOML via the [`TomlLiteralAttribute`](../attributes/toml-literal-attribute.md).

Strings can be serialized as multiline strings in TOML via the [`TomlMultilineAttribute`](../attributes/toml-multiline-attribute.md).

## Deserialization

String values are deserialized from TOML as any of the valid TOML string types (basic, literal, or multiline).

## Example

```csharp
[Serializable]
public class Quest
{
    private string _name;
    
    [TomlMultiline]
    private string _description;
    
    [TomlLiteral]
    private string _scriptPath;
}
```

Can be serialized and deserialized as the following TOML document:

```toml
name = "The Quest for the Holy Grail"
description = """
The Quest for the Holy Grail is a 1975 British musical comedy film directed and co-written by
Terry Gilliam and Terry Jones, and starring Graham Chapman, John Cleese, Eric Idle, Terry Gilliam,
Terry Jones, Michael Palin, and Carol Cleveland."""
scriptPath = 'C:\Users\Arthur\Documents\quest.py'
```
