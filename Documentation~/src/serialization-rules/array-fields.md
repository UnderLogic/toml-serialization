# Array Fields

## Overview

An array field is a field that is an array of scalars, complex objects, arrays, lists, or dictionaries.
Arrays are fixed length.

## Definition

```csharp
[Serializable]
public class MyData
{
    private int[] _scores;
    private string[] _names;
    private object[] _metadata;
}
```

## Mapping

An array field is mapped to either an [inline TOML array](https://toml.io/en/v1.0.0#array) or a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables) based on the type of the array elements.

Mixed types are allowed, but should be boxed to `object` and will serialized as their TOML representation.

### Scalar Example

If the array contains scalar types, it is serialized as an [inline TOML array](https://toml.io/en/v1.0.0#array).

```toml
scores = [ 100, 50, 25 ]
```

### Object Example

If the array contains complex types, it is serialized as a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).

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

If the array contains mixed types using boxed `object` values, each value is serialized as its TOML representation.

```toml
metadata = [ 100, "Player 1", true ]
```
