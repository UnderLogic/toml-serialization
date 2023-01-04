# Value Coercion

## Overview

When deserialize a TOML document into an object, the TOML values are coerced into the target type of the object field when possible.

This is only possible when the target type can be converted using a normal cast operation.
It is **not** possible to convert between types that are not implicitly convertible.

**NOTE:** These are intentionally very limited to prevent silent errors when deserializing TOML documents.

## Float => Integer Values

When deserializing a floating point number into an integer field, the floating point number will be truncated to the nearest whole integer value.

### Example

```toml
radius = 3.14
```

This would be coerced into any integer field as `3`. Any fractional part of the number is discarded.

## Integer => Float Values

When deserializing an integer into a floating point number field, the integer will be converted to a floating point number.

### Example

```toml
radius = 3
```

This would be coerced into any floating point number field as `3.0`. There is no loss of precision in this case.
