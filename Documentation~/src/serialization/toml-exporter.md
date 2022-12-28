# TomlExporter

A `MonoBehaviour` class component.

## Description

Component that provides methods for exporting `ScriptableObject` data to TOML files from Unity scenes.

## Serialized Fields (Inspector)

- `targetObject : ScriptableObject` - The `ScriptableObject` to export to a TOML file.
- `outputFile : string` - The path to the TOML file to export to.
- `usePersistentDataPath : bool` - Whether to use the persistent data path for the exported TOML file.
- `ensureDirectoryExists : bool` - Whether to create the directory for the exported TOML file, if it does not exist.

**NOTE:** The `outputFile` can include directories, but the directory must already exist unless `ensureDirectoryExists` is `true`.

## Public Events (Inspector)

- `onExport : UnityEvent` - The actions to perform when the export process completes successfully.
- `onError : UnityEvent<Exception>` - The actions to perform when the export process fails.

## Public Properties

- `TargetObject : ScriptableObject` **(get, set)** - The `ScriptableObject` to export to a TOML file.
- `OutputFile : string` **(get, set)** - The path to the TOML file to export to.
- `UsePersistentDataPath : bool` **(get, set)** - Whether to use the persistent data path for the exported TOML file.
- `EnsureDirectoryExists : bool` **(get, set)** - Whether to create the directory for the exported TOML file, if it does not exist.

## Public Methods

- `Export()` - Exports the source object to a TOML file with the default output file name.
- `ExportAs(string)` - Exports the source object to a TOML file with the specified file name.

## Output Directory

If `usePersistentDataPath` is `true`, the output directory will be appended to the persistent data path, otherwise it will be appended to the application data path.

| usePersistentDataPath | Resolved Path                                 |
|-----------------------|-----------------------------------------------|
| **true**              | `Application.persistentDataPath` + `filename` |
| **false**             | `Application.dataPath` + `filename`           |
