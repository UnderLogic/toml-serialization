# Field Selection

## Overview

By default, all public **and** non-public instance fields of an object are serialized, unless they are marked with the `NonSerialized` attribute.

This does **not** include properties, only member fields.

This is because properties are not guaranteed to be backed by a field, and the backing field may not be accessible.

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
    
    [NonSerialized]
    private DateTime _lastSaveTime;
}
```

In the above example, the `_lastSaveTime` field is **not** serialized because it is marked with the `NonSerialized` attribute.

The `_name`, `_level`, `_health`, `_maxHealth`, and `_gold` fields are serialized and will appear in the output TOML:

```toml
name = "Player 1"
level = 7
health = 160
maxHealth = 200
gold = 1250
```

**NOTE:** The underscore (`_`) at the beginning of the field name is removed when serializing.

See [Field Naming](field-naming.md) for more information.
