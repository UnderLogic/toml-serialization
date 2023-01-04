# TomlSnakeCase

## Overview

The `TomlSnakeCaseAttribute` can be applied to any `class`, `struct`, or `field`.
It will cause the serializer to use `snake_case` when serializing the value or values to TOML.

This is the default string casing so applying this attribute has no effect unless you have changed the default string casing for a `class` or `struct`.

## Effect

- When applied to a `class` or `struct`, it will cause the serializer to use `snake_case` when writing each field of the class or struct to TOML.
- When applied to a `field`, it will cause the serializer to use `snake_case` when writing the key of the field to TOML.

By default, it will be serialized as lowercase. This can be changed by passing `true` in the constructor for `UPPER_SNAKE_CASE` instead.

**NOTE:** This is will be ignored if the [`TomlKeyAttribute`](toml-key-attribute.md) is applied to the field.

## Inheritance

This attribute is **not** inherited by derived classes. However, fields of base classes marked with this attribute will still be serialized using `snake_case`.

It is also not propagated to child objects when serializing nested complex types.
You must apply this attribute to other complex types within the object graph if you want them to be serialized using `snake_case`.

## Public Properties

- `IsUpperCase : bool` **(get)** - Whether the string should be serialized as an uppercase string.

## Usage

### Class Example

```csharp
[Serializable]
[TomlSnakeCase]
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
quest_name = "Gather 10 Wood"
quest_description = "Gather 10 pieces of wood."
min_level = 1
is_repeatable = true
```

Notice that the keys of the fields are serialized using `snake_case`.

### Field Example

```csharp
[Serializable]
public class Quest
{
    [TomlSnakeCase]
    private int _questId;
    
    private string _questName;
    private string _questDescription;
    private int _minLevel;
    private bool _isRepeatable;
}
```

Would serialize into...

```toml
quest_id = 1
questName = "Gather 10 Wood"
questDescription = "Gather 10 pieces of wood."
minLevel = 1
isRepeatable = true
```

Notice that the key of the `_questId` field is serialized using `snake_case` while the other fields are serialized using the default `camelCase`.
