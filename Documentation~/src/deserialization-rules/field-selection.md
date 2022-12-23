# Field Selection

## Overview

By default, all public **and** non-public instance fields of an object are deserialized.

This does **not** include properties, only member fields.

This is because properties are not guaranteed to be backed by a field, and the backing field may not be accessible.

## Excluding Fields

If you wish to exclude a field from being deserialized, you can mark it with the `NonSerialized` attribute.
It will not be deserialized, and will not be modified.
