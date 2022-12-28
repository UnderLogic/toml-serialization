using UnityEditor;
using UnityEngine;

namespace UnderLogic.Serialization.Toml.Editor
{
    [CustomEditor(typeof(TomlImporter), true)]
    public class TomlImporterEditor : UnityEditor.Editor
    {
        private static readonly GUILayoutOption[] ButtonLayout =
        {
            GUILayout.MinWidth(192),
            GUILayout.MinHeight(28),
        };

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(4);
            if (GUILayout.Button("Import from File", ButtonLayout))
            {
                if (target is TomlImporter importer)
                    importer.Import();
            }
        }
    }
}
