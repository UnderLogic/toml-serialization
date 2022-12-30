# List Fields

## Overview

A list field is a field that is a list of scalars, complex objects, arrays, lists, or dictionaries.
Lists are of variable length.

## Definition

```csharp
[Serializable]
public class MyData
{
    private List<int> _scores;
    private List<string> _names;
    private List<object> _metadata;
}
```

**NOTE:** You should always use a generic list (`List<T>`) type when possible over an array type (`T[]`).

## Mapping

A list field is deserialized either from an [inline TOML array](https://toml.io/en/v1.0.0#array) or a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables) based on TOML document.

Mixed types are allowed, but will be boxed to `object` and will deserialized as their TOML representation.

## Coercion

There is no coercion for list fields.
However, the deserialize will attempt to coerce the TOML array to the type of the list elements defined in the object.

### Scalar Example

If the list defines a scalar type as its element type, it is deserialized from an [inline TOML array](https://toml.io/en/v1.0.0#array).

```toml
scores = [ 100, 50, 25 ]
```

### Object Example

If the list defines a complex type as its element type, it is deserialized from a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).

```toml
[[leaderboard]]
name = "Player 1"
score = 100

[[leaderboard]]
name = "Player 2"
score = 50
```

**NOTE:** Nested objects must be marked with the `Serializable` attribute.

### Mixed Example

If the list defines the `object` type as its element type, each value is deserialized as its TOML representation and boxed into an `object` reference.

```toml
metadata = [ 100, "Player 1", true ]
```
