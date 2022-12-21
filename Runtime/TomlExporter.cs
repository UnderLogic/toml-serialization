using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace UnderLogic.Serialization.Toml
{
    public class TomlExporter : MonoBehaviour
    {
        [SerializeField] private ScriptableObject targetObject;

        [Header("Output")]
        [SerializeField] private bool usePersistentDataPath = true;
        [SerializeField] private bool ensureDirectoryExists = true;
        [SerializeField] private string outputDirectory = "saves";
        [SerializeField] private string defaultFilename = "output.toml";

        [Space]
        public UnityEvent onExport;
        public UnityEvent<Exception> onError;

        public ScriptableObject TargetObject
        {
            get => targetObject;
            set => targetObject = value;
        }

        public bool UsePersistentDataPath
        {
            get => usePersistentDataPath;
            set => usePersistentDataPath = value;
        }

        public bool EnsureDirectoryExists
        {
            get => ensureDirectoryExists;
            set => ensureDirectoryExists = value;
        }

        public string OutputDirectory
        {
            get => outputDirectory;
            set => outputDirectory = value;
        }

        public string DefaultFilename
        {
            get => defaultFilename;
            set => defaultFilename = value;
        }

        public void Export() => ExportAs(defaultFilename);

        public void ExportAs(string filename)
        {
            if (targetObject == null)
            {
                Debug.LogWarning("No target object set, unable to export", this);
                return;
            }

            try
            {
                var outputPath = GetOutputPath(filename);
                if (EnsureDirectoryExists)
                    SafeMakeDirectory(Path.GetDirectoryName(outputPath));

                using (var stream = File.OpenWrite(outputPath))
                    TomlSerializer.Serialize(stream, targetObject);

                onExport?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
                onError?.Invoke(e);
            }
        }

        private string GetOutputPath(string filename) =>
            usePersistentDataPath
                ? Path.Combine(Application.persistentDataPath, OutputDirectory, filename)
                : Path.Combine(OutputDirectory, filename);

        private void SafeMakeDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return;

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }
}
