    using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(DescriptiveToggleAttribute))]
public class DescriptiveToggleDrawer : PropertyDrawer
{
    bool value;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        if (property.propertyType == SerializedPropertyType.Boolean)
        {
            EditorGUI.BeginProperty(position, label, property);
            var description = (attribute as DescriptiveToggleAttribute).GetString(property.boolValue);
            EditorGUI.BeginChangeCheck();
            value = EditorGUI.Toggle(position, label, property.boolValue);
            GUI.Label(new Rect(position.x + EditorGUIUtility.fieldWidth + EditorGUIUtility.labelWidth - 30, position.y, position.width / 3, position.height), description);
            if (EditorGUI.EndChangeCheck())
            {
                property.boolValue = value;
            }
            EditorGUI.EndProperty();
        }
        else
            EditorGUI.LabelField(position, label.text, "Use DescriptiveToggle for booleans.");
        EditorGUI.indentLevel = indent;
    }
}
