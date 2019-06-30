using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
namespace Our
{
    [CustomPropertyDrawer(typeof(PopUpAttribute))]
    public class PopUpPropertyDrawer : PropertyDrawer
    {
        private int index = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            PopUpAttribute items = attribute as PopUpAttribute;
            if (property.propertyType == SerializedPropertyType.String)
            {
                for(int i=0;i<items.items.Length;++i)
                    if (items.items[i]==property.stringValue)
                    {
                        index = i;
                    }
                EditorGUI.BeginProperty(position, label, property);
                var prevIndex = index;
                index = EditorGUI.Popup(position, property.displayName, index, items.items);
                if (prevIndex != index)
                {
                    property.stringValue = items.items[index];
                }
                EditorGUI.EndProperty();
            }
            else
                EditorGUI.LabelField(position, label.text, "Use PopUp for strings.");
            EditorGUI.indentLevel = indent;
        }

    }
}
