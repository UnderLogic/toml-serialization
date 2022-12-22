# Field Selection

## Overview

By default, all public **and** non-public instance fields of an object are deserialized, unless they are marked with the `NonSerialized` attribute.

This does **not** include properties, only member fields.

This is because properties are not guaranteed to be backed by a field, and the backing field may not be accessible.
