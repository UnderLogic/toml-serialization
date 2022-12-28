using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace UnderLogic.Serialization.Toml
{
    public class TomlImporter : MonoBehaviour
    {
        [SerializeField] private ScriptableObject targetObject;
        
        [Header("Import")]
        [SerializeField] private string inputFile = "output.toml";
        
        [Header("Options")]
        [SerializeField] private bool usePersistentDataPath = true;

        [Space]
        public UnityEvent onImport;
        public UnityEvent<Exception> onError;
        
        public ScriptableObject TargetObject
        {
            get => targetObject;
            set => targetObject = value;
        }

        public string InputFile
        {
            get => inputFile;
            set => inputFile = value;
        }
        
        public bool UsePersistentDataPath
        {
            get => usePersistentDataPath;
            set => usePersistentDataPath = value;
        }

        public void Import() => ImportFrom(InputFile);

        public void ImportFrom(string filename)
        {
            if (TargetObject == null)
            {
                Debug.LogWarning("No target object set, unable to import TOML", this);
                return;
            }

            try
            {
                var filePath = GetInputPath(filename);
                
                using (var stream = File.OpenRead(filePath))
                    TomlSerializer.DeserializeInto(stream, TargetObject);
                
                onImport?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
                onError?.Invoke(e);
            }
        }
        
        private string GetInputPath(string filename) =>
            UsePersistentDataPath
                ? Path.Combine(Application.persistentDataPath, filename)
                : Path.Combine(Application.dataPath, filename);
    }
}
