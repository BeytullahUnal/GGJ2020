using Pamir;
using UnityEditor;
using UnityEngine;

namespace Editor.PamirEditor {
    [CustomEditor(typeof(RewindableObjectGroup))]
    public class RewindableObjectGroupEditor : UnityEditor.Editor
    {
        private RewindableObjectGroup rewindableObjectGroup;
        
        private void OnEnable()
        {
            rewindableObjectGroup = (RewindableObjectGroup) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Release"))
            {
                rewindableObjectGroup.ReleaseObjects();
            }

            if (GUILayout.Button("Rewind"))
            {
                rewindableObjectGroup.RecallObjects();
            }
        }
    }
}
