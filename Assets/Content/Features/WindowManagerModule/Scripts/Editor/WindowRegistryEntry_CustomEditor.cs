// using UnityEditor;
// using UnityEditor.UIElements;
// using UnityEngine.UIElements;
//
// namespace Content.Features.WindowManagerModule.Scripts.Editor
// {
//     [CustomPropertyDrawer(typeof(WindowRegistryEntry))]
//     public class WindowRegistryEntry_CustomEditor : PropertyDrawer
//     {
//         public override VisualElement CreatePropertyGUI(SerializedProperty property)
//         {
//             return base.CreatePropertyGUI(property);
//             // var container = new VisualElement();
//             //
//             // // Create property fields.
//             // var prefabField = new PropertyField(property.FindPropertyRelative("WindowPrefab"), "Prefab");
//             // var keyField = new PropertyField(property.FindPropertyRelative("WindowKey"), "Key");
//             // keyField.SetEnabled(false);
//             //
//             // // Add fields to the container.
//             // container.Add(prefabField);
//             // container.Add(keyField);
//             //
//             // return container;
//         }
//     }
// }