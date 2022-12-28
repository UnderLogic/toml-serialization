using UnityEditor;
using UnityEngine;

namespace UnderLogic.Serialization.Toml.Editor
{
    [CustomEditor(typeof(TomlExporter), true)]
    public class TomlExporterEditor : UnityEditor.Editor
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
            if (GUILayout.Button("Export to File", ButtonLayout))
            {
                if (target is TomlExporter exporter)
                    exporter.Export();
            }
        }
    }
}
