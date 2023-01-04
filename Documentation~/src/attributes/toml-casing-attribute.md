# TomlCasingAttribute

## Overview

The `TomlCasingAttribute` can be used to override the case of the key in the TOML document.
It can be applied on the entire `class`, `struct`, or an individual `field`.

## Effect 

- When applied to a `class` or `struct`, it will set the default case for all fields.
  - It is **not** recursively applied to all child objects.
- When applied to a `field`, it will override the case of the key for that field only.

**NOTE**: The `TomlCasingAttribute` is ignored when the [`TomlKeyAttribute`](../attributes/toml-key-attribute.md) is used on a field.

## Supported Cases

The following cases are supported via the `StringCasing` enum value:

| Casing     | Field Name       | Output Key        |
|------------|------------------|-------------------|
| Default    | `_someFieldName` | `someFieldName`   |
| LowerCase  | `_someFieldName` | `somefieldname`   |
| UpperCase  | `_someFieldName` | `SOMEFIELDNAME`   |
| CamelCase  | `_someFieldName` | `someFieldName`   |
| PascalCase | `_someFieldName` | `SomeFieldName`   |
| SnakeCase  | `_someFieldName` | `some_field_name` |
| KebabCase  | `_someFieldName` | `some-field-name` |

**NOTE:** The leading underscores (`_`) are removed from the key regardless of the casing.

## Public Properties

- `Casing: StringCasing` **(get)** - The casing to use for the key.

## Usage

### Class Example

```csharp
[Serializable]
[TomlCasing(StringCasing.SnakeCase)]
public class PlayerData
{
    private string _playerName;
    private int _level;
    private int _currentHealth;
    private int _maxHealth;
    private int _currentMana;
    private int _maxMana;
    private int _gold;
    private int _statPoints;
}
```

Would serialize into...

```toml
player_name = "Player 1"
level = 7
current_health = 160
max_health = 200
current_mana = 100
max_mana = 120
gold = 1250
stat_points = 12
```

Notice that **all** of the fields of the class are serialized using snake case.

### Field Example

```csharp
[Serializable]
public class PlayerData
{
    private string _playerName;
    private int _level;
    private int _currentHealth;
    private int _maxHealth;
    private int _gold;
    private int _statPoints;

    [TomlCasing(StringCasing.KebabCase)]
    private DateTime _lastLoginDate;
}
```

Would serialize into...

```toml
playerName = "Player 1"
level = 7
currentHealth = 160
maxHealth = 200
gold = 1250
statPoints = 12
last-login-date = 2020-01-01 00:00:00Z
```

Notice that **only** the `_lastLoginDate` field is serialized using kebab case.
