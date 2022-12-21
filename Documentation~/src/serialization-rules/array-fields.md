# Array Fields

## Overview

An array field is a field that is an array or `IEnumerable` of scalars, objects, or other arrays.

Custom objects that implement `IEnumerable<T>` are also supported if the `T` type is one of the supported types.

## Mapping

An array field is mapped to either an [inline TOML array](https://toml.io/en/v1.0.0#array) or a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables) based on the type of the array elements.

Mixed types are allowed, but will be boxed to `object` and serialized as their TOML representation.

### Scalar Example

If the array contains scalar types, it is serialized as an [inline TOML array](https://toml.io/en/v1.0.0#array).

```toml
scores = [ 100, 50, 25 ]
```

### Object Example

If the array contains object types, it is serialized as a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).

```toml
[[leaderboard]]
name = "Player 1"
score = 100

[[leaderboard]]
name = "Player 2"
score = 50
```

**NOTE:** Nested objects must be marked with the `Serializable` attribute.
