# Serialize to File

## Overview

Once you have [defined a model](define-a-model.md), you can serialize it to a TOML file.
You can also load it from a TOML file if you have a model that matches the structure of the file.

In this example, we are going to assume this is the first time we are serializing this model so no file exists yet.

## Serializer Example

In order to serialize a model to a TOML file, you need to use the [`TomlSerializer`](../serialization/toml-serializer.md) `Serialize` static method.

To do this in Unity, we can create a simple `MonoBehaviour` that does this on the `Start` method.

Alternatively, you can use the [`TomlExporter`](../serialization/toml-exporter.md) which does this for you, assuming your data is a `ScriptableObject`.

```csharp
using System.IO;
using UnityEngine;

public class PlayerSettingsSaver : MonoBehaviour
{
    private PlayerSettings _playerSettings;

    private void Awake()
    {
        // Initialize the player settings
        _playerSettings = new PlayerSettings();
    }

    private void Start()
    {
        // Save the player settings to a TOML file in the persistent data path
        var filePath = Path.Combine(Application.persistentDataPath, "player-settings.toml");
        TomlSerializer.Serialize(settings, filePath);
    }
}
```

As you can see, we are creating a new instance of the `PlayerSettings` class and then serializing it to a file.

Obviously, you would want to do this when the player saves their settings, but for the sake of this example, we are doing it on the `Start` method.

## TOML Output

```toml
playerName = "Player"
credits = 0
difficultyLevel = "Normal"
soundVolume = 1.0
musicVolume = 1.0
```

## Load from a File

In the [next step](deserialize-from-file.md), we will load the `PlayerSettings` model from a TOML file.
