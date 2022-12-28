# TomlImporter

A `MonoBehaviour` class component.

## Description

Component that provides methods for importing TOML data into `ScriptableObject` instances from Unity scenes.

## Serialized Fields (Inspector)

- `targetObject : ScriptableObject` - The `ScriptableObject` to import TOML data into.
- `inputFile : string` - The path to the TOML file to import from.
- `usePersistentDataPath : bool` - Whether to use the persistent data path for the imported TOML file.

**NOTE:** The `inputFile` can include directories, but the directory must already exist.

## Public Events (Inspector)

- `onImport : UnityEvent` - The actions to perform when the import process completes successfully.
- `onError : UnityEvent<Exception>` - The actions to perform when the import process fails.

## Public Properties

- `TargetObject : ScriptableObject` **(get, set)** - The `ScriptableObject` to import TOML data into.
- `InputFile : string` **(get, set)** - The path to the TOML file to import from.
- `UsePersistentDataPath : bool` **(get, set)** - Whether to use the persistent data path for the imported TOML file.

## Public Methods

- `Import()` - Imports TOML data from a file with the default input file name.
- `ImportFrom(string)` - Imports TOML data from a file with the specified file name.

## Input Directory

If `usePersistentDataPath` is `true`, the input directory will be appended to the persistent data path, otherwise it will be appended to the application data path.

| usePersistentDataPath | Resolved Path                                 |
|-----------------------|-----------------------------------------------|
| **true**              | `Application.persistentDataPath` + `filename` |
| **false**             | `Application.dataPath` + `filename`           |
