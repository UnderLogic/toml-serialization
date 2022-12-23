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

**NOTE:** You should always use a generic list (`List<T>`) type when possible.

## Mapping

A list field is mapped to either an [inline TOML array](https://toml.io/en/v1.0.0#array) or a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables) based on the type of the array elements.

Mixed types are allowed, but should be boxed to `object` and will serialized as their TOML representation.

### Scalar Example

If the list contains scalar types, it is serialized as an [inline TOML array](https://toml.io/en/v1.0.0#array).

```toml
scores = [ 100, 50, 25 ]
```

### Object Example

If the list contains complex types, it is serialized as a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).

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

If the list contains mixed types using boxed `object` values, each value is serialized as its TOML representation.

```toml
metadata = [ 100, "Player 1", true ]
```
