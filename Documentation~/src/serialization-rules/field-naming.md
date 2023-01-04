# Field Naming

## Overview

This library uses reflection to determine the name of each field when serializing an object into TOML.

By default, the name of each field is used as the key in the TOML document.
This can be overridden by using the [`TomlKeyAttribute`](attributes/toml-key-attribute.md).

Any leading underscores (`_`) are removed from the field name before it is used as the key in the TOML document, unless explicitly named.

## String Casing

By default, each field is named as-is using the same casing as the field name.

This can be overriden by using one of the casing attributes:

- [`TomlCamelCaseAttribute`](../attributes/toml-camel-case-attribute.md)
- [`TomlPascalCaseAttribute`](../attributes/toml-pascal-case-attribute.md)
- [`TomlSnakeCaseAttribute`](../attributes/toml-snake-case-attribute.md)
- [`TomlKebabCaseAttribute`](../attributes/toml-kebab-case-attribute.md)

### Example

```csharp
[Serializable]
public class PlayerCharacter
{
    private string _name;
    private int _level;
    private int _experience;
}
```

This object will be serialized to the following TOML document:

```toml
name = "Hero"
level = 7
experience = 1250
```

Notice that the leading underscore (`_`) was removed from the field names in the TOML document.
