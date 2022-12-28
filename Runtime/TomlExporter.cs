using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace UnderLogic.Serialization.Toml
{
    public class TomlExporter : MonoBehaviour
    {
        [SerializeField] private ScriptableObject targetObject;

        [Header("Export")]
        [SerializeField] private string outputFile = "output.toml";
        
        [Header("Options")]
        [SerializeField] private bool usePersistentDataPath = true;
        [SerializeField] private bool ensureDirectoryExists = true;

        [Space]
        public UnityEvent onExport;
        public UnityEvent<Exception> onError;

        public ScriptableObject TargetObject
        {
            get => targetObject;
            set => targetObject = value;
        }

        public string OutputFile
        {
            get => outputFile;
            set => outputFile = value;
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
        
        public void Export() => ExportAs(OutputFile);

        public void ExportAs(string filename)
        {
            if (TargetObject == null)
            {
                Debug.LogWarning("No target object set, unable to export TOML", this);
                return;
            }

            try
            {
                var filePath = GetOutputPath(filename);
                if (EnsureDirectoryExists)
                    SafeMakeDirectory(Path.GetDirectoryName(filePath));

                using (var stream = File.OpenWrite(filePath))
                    TomlSerializer.Serialize(stream, TargetObject);

                onExport?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
                onError?.Invoke(e);
            }
        }

        private string GetOutputPath(string filename) =>
            UsePersistentDataPath
                ? Path.Combine(Application.persistentDataPath, filename)
                : Path.Combine(Application.dataPath, filename);

        private void SafeMakeDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return;

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }
}
