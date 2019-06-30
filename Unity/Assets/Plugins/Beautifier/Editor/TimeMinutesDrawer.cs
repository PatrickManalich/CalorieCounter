using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(TimeMinutesAttribute))]
public class TimeMinutesDrawer : PropertyDrawer
{
    private int minutes;
    private int seconds;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            minutes = property.intValue / 60;
            seconds = property.intValue % 60;
            EditorGUI.PrefixLabel(position, label);
            EditorGUI.BeginChangeCheck();
            EditorGUI.indentLevel = 1;
            minutes = EditorGUI.IntField(new Rect(position.x, position.y + EditorGUI.GetPropertyHeight(property), position.width, 15), "Minutes", minutes);
            seconds = EditorGUI.IntField(new Rect(position.x, position.y + EditorGUI.GetPropertyHeight(property) * 2, position.width, 15), "Seconds", seconds);
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = minutes * 60 + seconds;
            }
        }
        else
            EditorGUI.LabelField(position, label.text, "Use TimeMinutes for integer values.");
        EditorGUI.indentLevel = indent;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property) * 3;
    }

}
