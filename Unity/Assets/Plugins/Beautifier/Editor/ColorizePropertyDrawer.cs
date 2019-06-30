using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(ColorizeAttribute))]
public class ColorizePropertyDrawer : PropertyDrawer
{

    //private string prevString;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        EditorGUI.BeginProperty(position, label, property);
        GUI.color = (attribute as ColorizeAttribute).GetColor();
        EditorGUI.PropertyField(position, property, label, true);
        GUI.color = Color.white;
        EditorGUI.EndProperty();
        EditorGUI.indentLevel = indent;
    }
}
