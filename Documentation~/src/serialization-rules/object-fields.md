# Object Fields

## Overview

An object field is a field that is a `class` or `struct`, and is not an array, enumerable, or dictionary.
The object must be marked with the `Serializable` attribute.

Unity's built-in `MonoBehaviour` and `ScriptableObject` types are supported.

## Mapping

An object field can be mapped to either an [inline TOML table](https://toml.io/en/v1.0.0#inline-table) or a [standard TOML table](https://toml.io/en/v1.0.0#table).

### Inline Example

```toml
location = { index = 0, x = 100, y = 200, z = 300 }
```

### Standard Example

```toml
[location]
index = 0
x = 100
y = 200
z = 300
```

### Nested Objects

```toml
name = "Player 1"
level = 7
health = 160
gold = 1250

[[inventory]]
name = "Sword"
type = "Weapon"
quantity = 1
maxQuantity = 1

[[inventory]]
name = "Potion"
type = "Consumable"
quantity = 3
maxQuantity = 10
```

**NOTE:** Nested objects must be marked with the `Serializable` attribute.