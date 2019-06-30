using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(UpperCaseStringAttribute))]
public class UpperCaseStringPropertyDrawer : PropertyDrawer
{
    private string str;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            str = EditorGUI.TextField(position, property.displayName, property.stringValue);
            if (EditorGUI.EndChangeCheck())
            {
                str = str.ToUpper();
                property.stringValue = str;
            }
            EditorGUI.EndProperty();
        }
        else
            EditorGUI.LabelField(position, label.text, "Use UpperCaseString for strings.");
        EditorGUI.indentLevel = indent;
    }
}
