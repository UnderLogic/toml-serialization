# Field Ordering

## Overview

By default, the fields of an object are serialized in the order they are declared in the source code.
For key-value pairs, the key is serialized first, followed by the value.

## Serialization Order

1. Any key-value pairs are serialized first, including inline arrays, lists, and dictionaries.
2. Any nested tables are serialized next.
3. Any nested table arrays are serialized last.

## Nested Objects

A nested complex type is serialized after the parent object's fields.

### Example

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _health;
    private int _maxHealth;
    private int _gold;

    private PlayerStats _stats;
}
```

Would serialize into...

```toml
name = "Player 1"
level = 7
health = 160
maxHealth = 200
gold = 1250

[stats]
strength = 10
dexterity = 8
intelligence = 5
wisdom = 7
charisma = 6
```

Here, the `stats` table is serialized after the parent object's fields.

## Nested Object Arrays

A nested complex type array is serialized after the parent object's fields, and after any nested objects.

### Example

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _health;
    private int _maxHealth;
    private int _gold;

    private PlayerStats _stats;
    private InventoryItem[] _inventory;
}
```

Would serialize into...

```toml
name = "Player 1"
level = 7
health = 160
maxHealth = 200
gold = 1250

[stats]
strength = 10
dexterity = 8
intelligence = 5
wisdom = 7
charisma = 6

[[inventory]]
name = "Health Potion"
quantity = 3

[[inventory]]
name = "Mana Potion"
quantity = 2
```

Here, the `inventory` table array is serialized after the `stats` table.
