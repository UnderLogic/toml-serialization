# Field Naming

## Overview

By default, the name of the field is used as the key in the TOML document.

Any underscores (`_`) at the beginning of the field name are removed.

For example, the field name `_name` would be serialized as `name` in the output.

## Example

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

In the above example, the `_name`, `_level`, `_health`, `_maxHealth`, and `_gold` fields are serialized and will appear in the output TOML:

```toml
name = "Player 1"
level = 7
health = 160
maxHealth = 200
gold = 1250
```
