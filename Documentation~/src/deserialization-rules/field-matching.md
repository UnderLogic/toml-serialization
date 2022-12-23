# Field Matching

## Overview

By default, the name of the key is used to match the field.

Any underscores (`_`) at the beginning of the field name are removed and ignored when matching.

### Example

```toml
name = "Player 1"
level = 7
health = 160
maxHealth = 200
gold = 1250
```

Would deserialize into...

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _health;
    private int _maxHealth;
    private int _gold;
}
```

In the above example, the `_name`, `_level`, `_health`, `_maxHealth`, and `_gold` fields are deserialized and will be set in the output object.

## Nested Fields

Nested fields are deserialized using the same rules as the top-level object.
Nested tables are named using the dot (`.`) notation, if nested more than one level deep.

**NOTE:** Nested objects must also be marked with the `Serializable` attribute.

### Example

```toml
name = "Player 1"
level = 7
health = 160
maxHealth = 200
gold = 1250

[stats]
strength = 6
dexterity = 4
intelligence = 3
wisdom = 2
constitution = 8
charisma = 1
```

Would deserialize into..

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

[Serializable]
public class PlayerStats
{
    private int _strength;
    private int _dexterity;
    private int _intelligence;
    private int _wisdom;
    private int _constitution;
    private int _charisma;
}
```

In the above example, the `_name`, `_level`, `_health`, `_maxHealth`, and `_gold` fields are deserialized and will be set in the output object.

The `_stats` field is also deserialized and will be set in the output object.
