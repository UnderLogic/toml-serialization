# TomlPascalCase

## Overview

The `TomlPascalCaseAttribute` can be applied to any `class`, `struct`, or `field`.
It will cause the serializer to use `PascalCase` when serializing the value or values to TOML.

This is the default string casing so applying this attribute has no effect unless you have changed the default string casing for a `class` or `struct`.

## Effect

- When applied to a `class` or `struct`, it will cause the serializer to use `PascalCase` when writing each field of the class or struct to TOML.
- When applied to a `field`, it will cause the serializer to use `PascalCase` when writing the key of the field to TOML.

**NOTE:** This is will be ignored if the [`TomlKeyAttribute`](toml-key-attribute.md) is applied to the field.

## Inheritance

This attribute is **not** inherited by derived classes. However, fields of base classes marked with this attribute will still be serialized using `PascalCase`.

It is also not propagated to child objects when serializing nested complex types.
You must apply this attribute to other complex types within the object graph if you want them to be serialized using `PascalCase`.

## Usage

### Class Example

```csharp
[Serializable]
[TomlPascalCase]
public class Quest
{
    private string _questName;
    private string _questDescription;
    private int _minLevel;
    private bool _isRepeatable;
}
```

Would serialize into...

```toml
QuestName = "Gather 10 Wood"
QuestDescription = "Gather 10 pieces of wood."
MinLevel = 1
IsRepeatable = true
```

Notice that the keys of the fields are serialized using `PascalCase`.

### Field Example

```csharp
[Serializable]
public class Quest
{
    [TomlPascalCase]
    private int _questId;
    
    private string _questName;
    private string _questDescription;
    private int _minLevel;
    private bool _isRepeatable;
}
```

Would serialize into...

```toml
QuestId = 1
questName = "Gather 10 Wood"
questDescription = "Gather 10 pieces of wood."
minLevel = 1
isRepeatable = true
```

Notice that the key of the `_questId` field is serialized using `PascalCase` while the other keys are serialized using the default casing.
