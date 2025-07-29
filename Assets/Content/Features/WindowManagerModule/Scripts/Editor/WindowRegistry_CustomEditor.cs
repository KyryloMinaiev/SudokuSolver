using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

namespace Content.Features.WindowManagerModule.Scripts.Editor
{
    [CustomEditor(typeof(WindowRegistry))]
    public class WindowRegistry_CustomEditor : UnityEditor.Editor
    {
        private SerializedObject _serializedObject;
        private SerializedProperty _windowRegistryEntries;

        private void OnEnable()
        {
            _serializedObject = new SerializedObject(target);
            _windowRegistryEntries = _serializedObject.FindProperty("WindowRegistryEntries");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_windowRegistryEntries);
            if (EditorGUI.EndChangeCheck())
            {
                _serializedObject.ApplyModifiedProperties();
                UpdateEntries();
            }
            
            _serializedObject.Update();
        }

        private void UpdateEntries()
        {
            WindowRegistryUtility.UpdateWindowRegistry(target as WindowRegistry);
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}