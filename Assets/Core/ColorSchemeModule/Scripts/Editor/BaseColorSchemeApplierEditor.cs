using UnityEditor;
using UnityEngine;

namespace Core.ColorSchemeModule.Scripts.Editor
{
    public abstract class BaseColorSchemeApplierEditor : UnityEditor.Editor
    {
        [SerializeField] ColorSchemeConfiguration _colorSchemeConfiguration;
        private SerializedProperty _colorRoleID;
        
        private void OnEnable()
        {
            if (_colorSchemeConfiguration == null)
            {
                TryFindColorSchemeConfiguration();
            }
        }

        public override void OnInspectorGUI()
        {
            DrawBaseColorSchemeApplierEditor();
        }

        private void TryFindColorSchemeConfiguration()
        {
            var configurationsGUIDs = AssetDatabase.FindAssets("t:ColorSchemeConfiguration");
            if (configurationsGUIDs.Length == 0)
            {
                return;
            }
            
            var configurationPath = AssetDatabase.GUIDToAssetPath(configurationsGUIDs[0]);
            _colorSchemeConfiguration = AssetDatabase.LoadAssetAtPath<ColorSchemeConfiguration>(configurationPath);
        }

        private void DrawBaseColorSchemeApplierEditor()
        {
            if (_colorSchemeConfiguration == null)
            {
                DrawConfigurationNotFoundError();
                return;
            }

            serializedObject.Update();
            _colorRoleID = serializedObject.FindProperty("_colorRoleID");
            
            DrawColorRoleSelector();
            DrawColorsForRole();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawColorRoleSelector()
        {
            var colorRoles = _colorSchemeConfiguration.ColorRoles;
            var colorRolesNames = colorRoles.ConvertAll(colorRole => colorRole.Name).ToArray();
            var colorRolesIDs = colorRoles.ConvertAll(colorRole => colorRole.ID).ToArray();
            _colorRoleID.intValue = EditorGUILayout.IntPopup("Select color role", _colorRoleID.intValue, colorRolesNames, colorRolesIDs);
        }

        private void DrawColorsForRole()
        {
            EditorGUILayout.LabelField("Colors for role:");
            EditorGUI.indentLevel++;
            GUI.enabled = false;
            var colorSchemes = _colorSchemeConfiguration.ColorSchemes;
            foreach (ColorScheme colorScheme in colorSchemes)
            {
                colorScheme.Initialize();
                Color color = colorScheme.GetColor(_colorRoleID.intValue);
                EditorGUILayout.ColorField(colorScheme.Name, color);
            }
            
            GUI.enabled = true;
            EditorGUI.indentLevel--;
        }

        private void DrawConfigurationNotFoundError()
        {
            EditorGUILayout.HelpBox("ColorSchemeConfiguration not found!", MessageType.Error);
        }
    }
}