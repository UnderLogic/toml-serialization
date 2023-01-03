# Field Ordering

## Overview

This library uses reflection to determine the order of each field to serialize and deserialize within an object.

By default, the fields are serialized and deserialized in the order they are declared in the object.

## Table Order

When writing the object as a TOML table, the fields are written in the following way:

1. All key-value pairs of the table are written.
2. All nested tables are written.
3. All nested table arrays are written.

This is done recursively for each nested table.

## Example

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _experience;
    private PlayerStats _stats;
    private List<InventoryItem> _inventory;
}
```

This object will be serialized to the following TOML document:

```toml
name = "Hero"
level = 7
experience = 1250

[stats]
health = 100
mana = 50

[[inventory]]
name = "Sword"
durability = 84

[[inventory]]
name = "Shield"
durability = 99
```

Notice that fields are written in the order they are declared in the object, and nested tables are written after the key-value pairs of the parent table.

Even if the `_stats` and `_inventory` fields were declared before the `_name`, `_level`, and `_experience` fields, they would still be written after them.
