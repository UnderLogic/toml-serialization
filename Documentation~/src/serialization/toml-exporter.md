# TomlExporter

A `MonoBehaviour` class component.

## Description

Component that provides methods for exporting `ScriptableObject` data to TOML files from Unity scenes.

## Serialized Fields (Inspector)

- `sourceObject : ScriptableObject` - The `ScriptableObject` to export to a TOML file.
- `usePersistentDataPath : bool` - Whether to use the persistent data path for the exported TOML file.
- `ensureDirectoryExists : bool` - Whether to create the directory for the exported TOML file, if it does not exist.
- `outputDirectory : string` - The output directory to append for the exported TOML file.
- `defaultFileName : string` - The default file name to use for the exported TOML file.

## Public Events (Inspector)

- `onExport : UnityEvent` - The actions to perform when the export process completes successfully.
- `onError : UnityEvent<Exception>` - The actions to perform when the export process fails.

## Public Properties

- `SourceObject : ScriptableObject` **(get, set)** - The `ScriptableObject` to export to a TOML file.
- `UsePersistentDataPath : bool` **(get, set)** - Whether to use the persistent data path for the exported TOML file.
- `EnsureDirectoryExists : bool` **(get, set)** - Whether to create the directory for the exported TOML file, if it does not exist.
- `OutputDirectory : string` **(get, set)** - The output directory to append for the exported TOML file.
- `DefaultFileName : string` **(get, set)** - The default file name to use for the exported TOML file.

## Public Methods

- `Export()` - Exports the source object to a TOML file with the default file name.
- `Export(string)` - Exports the source object to a TOML file with the specified file name.

## Output Directory

If `usePersistentDataPath` is `true`, the output directory will be appended to the persistent data path, otherwise it will be appended to the application data path.

| usePersistentDataPath | Resolved Path                                                     |
|-----------------------|-------------------------------------------------------------------|
| **true**              | `Application.persistentDataPath` + `outputDirectory` + `filename` |
| **false**             | `Application.dataPath` + `outputDirectory` + `filename`           |

**NOTE:** If the `outputDirectory` is `null` or empty, it will not be appended to the path.
