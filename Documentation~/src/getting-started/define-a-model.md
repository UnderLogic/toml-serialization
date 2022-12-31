# Define a Model

## Overview

The first step to using TOML in Unity is to define a model class (or struct) that represents the data you want to serialize or deserialize.

It can be as simple as a single class, or it can be a complex hierarchy of classes.
The serializer will automatically serialize and deserialize the entire object graph, if possible.

A common use case is to define a model class that represents a configuration file, let's say `PlayerSettings`:

## Player Settings Model

```csharp
[Serializable]
public class PlayerSettings
{
    private string _playerName = "Player";
    private int _credits;
    
    private string _difficultyLevel = "Normal";
    
    private float _soundVolume = 1.0f;
    private float _musicVolume = 1.0f;
    
    // ... other fields
    
    [NonSerialized]
    private DateTime _lastSavedAt;  // will not be serialized
}
```

The model class must be marked with the [`SerializableAttribute`](https://docs.microsoft.com/en-us/dotnet/api/system.serializableattribute) attribute, which tells the serializer to include the class in the serialization process.

If you have fields you do not want to be serialized, you can mark them with the [`NonSerializedAttribute`](https://docs.microsoft.com/en-us/dotnet/api/system.nonserializedattribute) attribute.
They will be ignored by the serializer and deserializer.

## Field Attributes

The TOML serializer supports a number of attributes that can be applied to fields to control how they are serialized and deserialized.

For more information on the attributes, see the [Attributes](../attributes/attributes.md) page.

## Constructors

It is recommended that you have a parameterless constructor for your model classes.
You can use constructor chaining to call a constructor with parameters, if you need to.

This is because the serializer will create an instance of your model class using the parameterless constructor, and then populate the fields using the deserialized data.
If you do not have a parameterless constructor, the serializer will throw an exception.

By default, all classes have a parameterless constructor, but if you have a custom constructor, you will need to add a parameterless constructor yourself.
All structs have a parameterless constructor as they are value types.

## Serializing to a File

In the [next step](./serialize-to-file.md), we will serialize the `PlayerSettings` model to a TOML file.
