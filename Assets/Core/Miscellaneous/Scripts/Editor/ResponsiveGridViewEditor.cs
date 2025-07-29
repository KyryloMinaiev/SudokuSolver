using UnityEditor;

namespace Content.Features.UIModule.Scripts.Editor
{
    [CustomEditor(typeof(ResponsiveGridView))]
    [CanEditMultipleObjects]
    public class ResponsiveGridViewEditor : UnityEditor.Editor
    {
        SerializedProperty _padding;
        SerializedProperty _cellSizeConstraint;
        SerializedProperty _minCellSize;
        SerializedProperty _maxCellSize;
        SerializedProperty _cellSize;
        SerializedProperty _spacingSizeConstraint;
        SerializedProperty _minSpacingSize;
        SerializedProperty _maxSpacingSize;
        SerializedProperty _spacingSize;
        SerializedProperty _minColumnConstraintCount;
        SerializedProperty _maxColumnConstraintCount;
        SerializedProperty _columnConstraintCount;
        SerializedProperty _columnConstraint;
        SerializedProperty _childAlignment;

        protected virtual void OnEnable()
        {
            _padding = serializedObject.FindProperty("m_Padding");
            _cellSizeConstraint = serializedObject.FindProperty("_cellSizeConstraint");
            _minCellSize = serializedObject.FindProperty("_minCellSize");
            _maxCellSize = serializedObject.FindProperty("_maxCellSize");
            _cellSize = serializedObject.FindProperty("_cellSize");
            _spacingSizeConstraint = serializedObject.FindProperty("_spacingSizeConstraint");
            _minSpacingSize = serializedObject.FindProperty("_minSpacingSize");
            _maxSpacingSize = serializedObject.FindProperty("_maxSpacingSize");
            _spacingSize = serializedObject.FindProperty("_spacingSize");
            _minColumnConstraintCount = serializedObject.FindProperty("_minColumnConstraintCount");
            _maxColumnConstraintCount = serializedObject.FindProperty("_maxColumnConstraintCount");
            _columnConstraintCount = serializedObject.FindProperty("_columnConstraintCount");
            _columnConstraint = serializedObject.FindProperty("_columnConstraint");
            _childAlignment = serializedObject.FindProperty("m_ChildAlignment");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_padding, true);
            EditorGUILayout.PropertyField(_cellSizeConstraint, true);
            EditorGUI.indentLevel++;
            if (_cellSizeConstraint.enumValueIndex > 0)
            {
                EditorGUILayout.PropertyField(_minCellSize, true);
                EditorGUILayout.PropertyField(_maxCellSize, true);
                _spacingSizeConstraint.enumValueIndex = 0;
            }
            else
            {
                EditorGUILayout.PropertyField(_cellSize, true);
            }
            
            EditorGUI.indentLevel--;
            
            EditorGUILayout.PropertyField(_spacingSizeConstraint, true);
            EditorGUI.indentLevel++;
            if (_spacingSizeConstraint.enumValueIndex > 0)
            {
                EditorGUILayout.PropertyField(_minSpacingSize, true);
                EditorGUILayout.PropertyField(_maxSpacingSize, true);
                _cellSizeConstraint.enumValueIndex = 0;
            }
            else
            {
                EditorGUILayout.PropertyField(_spacingSize, true);
            }
            
            EditorGUI.indentLevel--;
            
            EditorGUILayout.PropertyField(_childAlignment, true);
            EditorGUILayout.PropertyField(_columnConstraint, true);
            EditorGUI.indentLevel++;
            if (_columnConstraint.enumValueIndex > 0)
            {
                EditorGUILayout.PropertyField(_minColumnConstraintCount, true);
                EditorGUILayout.PropertyField(_maxColumnConstraintCount, true);
            }
            else
            {
                EditorGUILayout.PropertyField(_columnConstraintCount, true);   
            }
            
            EditorGUI.indentLevel--;
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}