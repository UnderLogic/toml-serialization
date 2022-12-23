# Under the Hood

## Overview

This document is intended for developers who wish to understand the inner workings of the TOML serializer.
It is not required reading for users of the library.

It also serves as documentation for the serialization and deserialization process at a more granular level.

All of these are marked as `internal` and not exposed to the public API of this library.
The only exception is the `TomlSerializer` class, which is the entry point for serialization and deserialization.

## Marshalling

The `TomlSerializer` uses an internal marshalling process to convert objects to and from TOML.
These objects serve as a bridge between the TOML document and the object being serialized or deserialized.

These are the `TomlValue` derived classes below:

- `TomlNull`
- `TomlBoolean`
- `TomlDateTime`
- `TomlFloat`
- `TomlInteger`
- `TomlString`
- `TomlArray`
- `TomlTable`
- `TomlTableArray`

The `TomlArray`, `TomlTable` and `TomlTableArray` can contain any of the above types as well (nested).

The `TomlTable` can be inline or standard (expanded). This is determined by the `TomlTable.IsInline` property.

## Serialization

The serialization process is broken down into three steps done by the `TomlSerializer` class:

    1. Iterate over the object's fields
    2. Marshal each field's value into a TOML value
    3. Write the TOML root table

### Field Iteration

The `TomlSerializer` class iterates over the fields of the object using reflection, ignoring any fields marked with the `NonSerialized` attribute.

It will recursively iterate over any nested objects, and supports arrays, lists, and dictionaries.

### Field Marshaling

The `TomlSerializer` class converts each field's value into a `TomlValue` based on the field's type.
See the above possible `TomlValue` types.

The value is added to the root table, child table, or child array using the field's name as the key.

### Root Table Writing

The `TomlSerializer` class writes the root table to the TOML document using the `TomlWriter` class.

The `TomlWriter` class is responsible for writing the TOML document to a string, stream, or writer and all of the formatting.

## Deserialization

The deserialization process is broken down into three steps done by the `TomlSerializer` class:

    1. Parse the TOML document into marshalled objects
    2. Create the deserialized object
    3. Unmarshal the TOML root table into the deserialized object

**NOTE:** In the `DeserializeInto` method, the deserialized object is passed in as a parameter instead of being created.

### TOML Document Parsing

The `TomlSerializer` class reads the TOML document using the `TomlReader` class.

The `TomlReader` class is responsible for reading the TOML document from a string, stream, or reader and parsing it into `TomlValue` objects.

### Object Creation

The `TomlSerializer` class creates the deserialized object using the `Activator` or `Array` static class methods.
This is done by calling the default constructor of the object's type.

### Field Deserialization

The `TomlSerializer` class converts each parsed `TomlValue` to the field's type and sets it.

This is done by calling helper methods on the `TomlConvert` static class.
This includes scalar values, complex types, arrays, lists, and dictionaries.
