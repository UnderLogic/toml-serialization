# Attributes

## Overview

Attributes are used to customize the serialization and deserialization behavior of a `class`, `struct`, or `field`.

## Composition

Many attributes can be composed together to provide a more specific behavior.

For example, the [`TomlLiteralAttribute`](attributes/toml-multiline-attribute.md) attribute can be used in conjunction with the [`TomlMultilineAttribute`](attributes/toml-multiline-attribute.md) attribute to specify that a string should be serialized as a literal string and that the string should be serialized as a multiline string.

## Inheritance

Attributes are **not** inherited by derived classes, but they are inherited by fields of derived classes.

For example, a derived class inheriting from a base class marked with the [`TomlCasingAttribute`](attributes/toml-casing-attribute.md)
will not inherit the casing behavior of the base class, but a field of the derived class will inherit the casing behavior of the base class.

## Non-Serialized Fields

Fields marked with the [`NonSerializedAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.nonserializedattribute?view=net-6.0) will not be serialized or deserialized.
