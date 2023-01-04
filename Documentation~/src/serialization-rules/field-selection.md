# Field Selection

## Overview 
This library uses reflection to determine which fields to serialize and deserialize within an object.

By default, all public **and** private instance fields are serialized and deserialized unless they are marked with the [`NonSerializedAttribute`](https://docs.microsoft.com/en-us/dotnet/api/system.nonserializedattribute?view=net-6.0).

Properties are **not** serialized or deserialized.
This is because properties may not have a backing field, or may be read-only.
