# Dictionary Fields

## Overview

A dictionary field is a field that is a `Dictionary<string, TValue>`, where `TValue` is a supported type.
`TValue` can be a scalar, object, array, list, or dictionary type.

## Mapping

A dictionary field is mapped to either an [inline TOML table](https://toml.io/en/v1.0.0#inline-table) or a [standard TOML table](https://toml.io/en/v1.0.0#table) based on the type of the dictionary values.

Mixed types are allowed, but will be boxed to `object` and serialized as their TOML representation.

### Scalar Example

If the dictionary contains scalar types, it is serialized as an [inline TOML table](https://toml.io/en/v1.0.0#inline-table).

```toml
scores = { math = 100, science = 90, english = 80 }
```

### Object Example

If the dictionary contains object types, it is serialized as a [standard TOML table](https://toml.io/en/v1.0.0#table).

```toml
[results]
id = 102
weather = { temperature = 100, humidity = 50 }
sunrise = { hour = 6, minute = 30 }
sunset = { hour = 18, minute = 30 }
tags = [ "hot", "sunny" ]
updatedAt = 2020-01-01 00:00:00Z
```

**NOTE:** Nested objects must be marked with the `Serializable` attribute.

### Array Example

If the dictionary contains array or list types, it is serialized as a [TOML table array](https://toml.io/en/v1.0.0#array-of-tables).

```toml
[[results]]
id = 102
weather = { temperature = 100, humidity = 50 }

[[results]]
id = 103
weather = { temperature = 90, humidity = 40 }
```
