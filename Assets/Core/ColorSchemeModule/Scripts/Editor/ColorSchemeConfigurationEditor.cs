using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.ColorSchemeModule.Scripts.Editor
{
    [CustomEditor(typeof(ColorSchemeConfiguration))]
    public class ColorSchemeConfigurationEditor : UnityEditor.Editor
    {
        private readonly List<int> _rolesMarkedForRemoval = new List<int>();
        private readonly List<int> _schemesMarkedForRemoval = new List<int>();
        private string _newRoleName;
        private string _newSchemeName;
        
        public override void OnInspectorGUI()
        {
            ColorSchemeConfiguration configuration = (ColorSchemeConfiguration) target;
            
            EditorGUI.BeginChangeCheck();
            DrawColorRolesEditor(configuration);
            DrawColorSchemesEditor(configuration);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(configuration);
            }
        }

        private void DrawColorRolesEditor(ColorSchemeConfiguration configuration)
        {
            EditorGUILayout.LabelField("Color roles:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            
            var colorRoles = configuration.ColorRoles;
            for (int i = 0; i < colorRoles.Count; i++)
            {
                DrawColorRoleEditor(colorRoles[i], i);
            }

            foreach (int markedForRemoval in _rolesMarkedForRemoval)
            {
                colorRoles.RemoveAt(markedForRemoval);
            }
            
            _rolesMarkedForRemoval.Clear();
            DrawAddRoleSection(colorRoles);
            
            EditorGUI.indentLevel--;
        }

        private void DrawColorRoleEditor(ColorRole colorRole, int index)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField(colorRole.Name);
            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                _rolesMarkedForRemoval.Add(index);
            }
                
            EditorGUILayout.EndHorizontal();
        }
        
        private void DrawAddRoleSection(List<ColorRole> colorRoles)
        {
            EditorGUILayout.BeginHorizontal();
            _newRoleName = EditorGUILayout.TextField(_newRoleName);
            if (GUILayout.Button("Add role"))
            {
                if (!string.IsNullOrEmpty(_newRoleName) && colorRoles.FindIndex(colorRole => colorRole.Name == _newRoleName) == -1)
                {
                    colorRoles.Add(new ColorRole(_newRoleName, _newRoleName.GetHashCode()));
                    _newRoleName = "";
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawColorSchemesEditor(ColorSchemeConfiguration configuration)
        {
            EditorGUILayout.LabelField("Color schemes:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            
            var colorSchemes = configuration.ColorSchemes;
            for (int i = 0; i < colorSchemes.Count; i++)
            {
                DrawColorSchemeEditor(colorSchemes[i], i, configuration.ColorRoles);
            }

            foreach (int markedForRemoval in _schemesMarkedForRemoval)
            {
                colorSchemes.RemoveAt(markedForRemoval);
            }
            
            _schemesMarkedForRemoval.Clear();
            DrawAddSchemeSection(colorSchemes);
            
            EditorGUI.indentLevel--;
        }

        private void DrawColorSchemeEditor(ColorScheme colorScheme, int index, List<ColorRole> colorRoles)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(colorScheme.Name);
            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                _schemesMarkedForRemoval.Add(index);
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel++;
            var colorRoleSettings = colorScheme.ColorRoleSettings;
            for (int i = colorRoleSettings.Count - 1; i >= 0; i--)
            {
                if (colorRoles.FindIndex(colorRole => colorRole.ID == colorRoleSettings[i].ColorRoleID) == -1)
                {
                    colorRoleSettings.RemoveAt(i);
                }
            }
            
            for (int i = 0; i < colorRoles.Count; i++)
            {
                if (colorRoleSettings.FindIndex(colorRoleSetting => colorRoleSetting.ColorRoleID == colorRoles[i].ID) == -1)
                {
                    colorRoleSettings.Add(new ColorRoleSettings(colorRoles[i].ID, Color.black));   
                }
            }

            for (var i = 0; i < colorRoleSettings.Count; i++)
            {
                var colorRoleSetting = colorRoleSettings[i];
                string colorRoleName = colorRoles.Find(colorRole => colorRole.ID == colorRoleSetting.ColorRoleID).Name;
                colorRoleSetting.Color = EditorGUILayout.ColorField(colorRoleName, colorRoleSetting.Color);
                colorRoleSettings[i] = colorRoleSetting;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        private void DrawAddSchemeSection(List<ColorScheme> colorSchemes)
        {
            EditorGUILayout.BeginHorizontal();
            _newSchemeName = EditorGUILayout.TextField(_newSchemeName);
            if (GUILayout.Button("Add color scheme"))
            {
                if (!string.IsNullOrEmpty(_newSchemeName) && colorSchemes.FindIndex(colorScheme => colorScheme.Name == _newSchemeName) == -1)
                {
                    colorSchemes.Add(new ColorScheme(_newSchemeName, new List<ColorRoleSettings>()));
                    _newSchemeName = "";
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}