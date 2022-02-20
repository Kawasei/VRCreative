#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Creation.HologramAdvertisement
{
    [CustomPropertyDrawer(typeof(Advertisement))]
    public class AdvertisementDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var labelRect = new Rect(position);
                labelRect.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.LabelField(labelRect, label.text, EditorStyles.boldLabel);
                position.y += EditorGUIUtility.singleLineHeight;
                drawField(ref position, property, "Texture", "texture");
                drawField(ref position, property, "AudioClip", "audioClip");
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("texture"));
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("audioClip"));
            return height;
        }
        
        private void drawField(ref Rect rect, SerializedProperty property, string label, string propertyName)
        {
            var foundProperty = property.FindPropertyRelative(propertyName);
            EditorGUI.PropertyField(rect, foundProperty, new GUIContent(label), true);
            rect.y += EditorGUI.GetPropertyHeight(foundProperty);
        }
    }
}
    
#endif