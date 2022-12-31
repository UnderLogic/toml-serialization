# Deserialize from File

## Overview

Once you have [defined a model](define-a-model.md), you can deserialize it from a TOML file.
In the [previous step](serialize-to-file.md), we serialized a `PlayerSettings` model to a TOML file.

In some cases you may only be consuming TOML files and not creating them.
You can still use the TOML serializer to deserialize a model from a TOML file.

## Deserializer Example

In order to deserialize a model from a TOML file, you need to use the [`TomlSerializer`](../serialization/toml-serializer.md) `Deserialize` or `DeserializeInto` static methods.

To do this in Unity, we can create a simple `MonoBehaviour` that does this on the `Start` method.

Alternatively, you can use the [`TomlImporter`](../deserialization/toml-importer.md) which does this for you, assuming your data is a `ScriptableObject`.

```csharp
using System.IO;
using UnityEngine;

public class PlayerSettingsLoader : MonoBehaviour
{
    private PlayerSettings _playerSettings;

    private void Awake()
    {
        // Initialize the player settings
        _playerSettings = new PlayerSettings();
    }

    private void Start()
    {
        // Load the player settings from a TOML file in the persistent data path
        var filePath = Path.Combine(Application.persistentDataPath, "player-settings.toml");
        using (var reader = new StreamReader(filePath))
        {
            TomlSerializer.DeserializeInto(reader, _playerSettings);
        }
    }
}
```

As you can see, we are creating a new instance of the `PlayerSettings` class and then deserializing it from a file.

Obviously, you would want to do this on application load but for the sake of this example, we are doing it on the `Start` method.

## Deserialize vs. DeserializeInto

There are two ways to deserialize a model from a TOML file, and they behave the same way.

The `Deserialize<T>` method will create a new instance of the model and return it.
The requirement is that the `T` being passed in is a class or struct that has a parameterless constructor.

In some cases, you may want to deserialize a model into an existing instance.
You can use the `DesrializeInto<T>` method for this.

This is common when you are loading a model from a file and then updating it with new values.
Or the type you are deserializing does not have a parameterless constructor.

## Scriptable Objects

One example of needing the `DeserializeInto` method this is when deserializing a `ScriptableObject` in Unity.
You may have an existing instance of a `ScriptableObject` that you want to deserialize into.

```csharp
// Assume PlayerSettings is a ScriptableObject

// Deserialize into an existing ScriptableObject
TomlSerializer.DeserializeInto(tomlString, _playerSettings);

// Deserialize into a new ScriptableObject
var newSettings = ScriptableObject.CreateInstance<PlayerSettings>();
tomlSerializer.DeserializeInto(tomlString, newSettings);

// This will not work with ScriptableObjects, use the above instead
var newSettings = TomlSerializer.Deserialize<PlayerSettings>(tomlString);
```

You can also do the same for deserializing into `MonotBehaviour` instances.
