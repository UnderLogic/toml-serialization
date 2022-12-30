# Object Fields

## Overview

An object field is a field that is a complex type and not an array, list, or dictionary.
These are typically `class` or `struct` types.
The object must be marked with the `Serializable` attribute.

Unity's built-in `MonoBehaviour` and `ScriptableObject` types are supported, but also be marked with the `Serializable` attribute.

**NOTE:** Boxed `object` values are handled as scalar fields, not object fields.

## Mapping

An complex type can be mapped to either an [inline TOML table](https://toml.io/en/v1.0.0#inline-table) or a [standard TOML table](https://toml.io/en/v1.0.0#table).

By default, the complex type will be serialized as a standard (expanded) table.
You can override this behavior by using the [`TomlInlineAttribute`](../attributes/toml-inline-attribute.md) attribute to serialize the complex type as an inline table.

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

**NOTE**: This also called an "expanded" table.

### Nested Example

Nested objects are serialized using child tables.

```toml
name = "Player 1"
level = 7
health = 160
gold = 1250

[stats]
strength = 10
dexterity = 8
intelligence = 6
wisdom = 4
constitution = 12
charisma = 5
```

### Array Example

Arrays of complex types are serialized as a table array.

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
