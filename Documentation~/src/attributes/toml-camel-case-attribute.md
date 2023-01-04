# TomlCamelCase

## Overview

The `TomlCamelCaseAttribute` can be applied to any `class`, `struct`, or `field`.
It will cause the serializer to use `camelCase` when serializing the value or values to TOML.

## Effect

- When applied to a `class` or `struct`, it will cause the serializer to use `camelCase` when writing each field of the class or struct to TOML.
- When applied to a `field`, it will cause the serializer to use `camelCase` when writing the key of the field to TOML.

**NOTE:** This is will be ignored if the [`TomlKeyAttribute`](toml-key-attribute.md) is applied to the field.

## Inheritance

This attribute is **not** inherited by derived classes. However, fields of base classes marked with this attribute will still be serialized using `camelCase`.

It is also not propagated to child objects when serializing nested complex types.
You must apply this attribute to other complex types within the object graph if you want them to be serialized using `camelCase`.

## Usage

### Class Example

```csharp
[Serializable]
[TomlCamelCase]
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
questName = "Gather 10 Wood"
questDescription = "Gather 10 pieces of wood."
minLevel = 1
isRepeatable = true
```

Notice that the keys of the fields are serialized using `camelCase`.

### Field Example

```csharp
[Serializable]
[TomlSnakeCase]
public class Quest
{
    [TomlCamelCase]
    private int _questId;
    
    private string _questName;
    private string _questDescription;
    private int _minLevel;
    private bool _isRepeatable;
}
```

Would serialize into...

```toml
questId = 1
quest_name = "Gather 10 Wood"
quest_description = "Gather 10 pieces of wood."
min_level = 1
is_repeatable = true
```

Notice that the key of the `_questId` field is serialized using `camelCase` even though the class is serialized using `snake_case`.
