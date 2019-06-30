using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(PasswordAttribute))]
public class PopUpPropertyDrawer : PropertyDrawer
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
            str = EditorGUI.PasswordField(position, property.displayName, property.stringValue);
            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = str;
            }
            EditorGUI.EndProperty();
        }
        else
            EditorGUI.LabelField(position, label.text, "Use Password for strings.");
        EditorGUI.indentLevel = indent;
    }

}
