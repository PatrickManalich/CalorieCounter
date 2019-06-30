using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReplaceCharacterAttribute))]
public class ReplaceCharacterPropertyDrawer : PropertyDrawer
{

    private string str;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);
            ReplaceCharacterAttribute replacementData = attribute as ReplaceCharacterAttribute;
            EditorGUI.BeginChangeCheck();
            str = EditorGUI.TextField(position, property.displayName, property.stringValue);
            if (EditorGUI.EndChangeCheck())
            {
                str = str.Replace(replacementData.oldChar, replacementData.newChar);
                property.stringValue = str;
            }
            EditorGUI.EndProperty();
        }
        else
            EditorGUI.LabelField(position, label.text, "Use ReplaceCharacter for strings.");
        EditorGUI.indentLevel = indent;
    }
}