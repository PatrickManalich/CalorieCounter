using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DisplayBasedOnBoolAttribute))]
public class DisplayBasedOnBoolPropertyDrawer : PropertyDrawer
{

    //private string prevString;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DisplayBasedOnBoolAttribute attr = attribute as DisplayBasedOnBoolAttribute;
        var boolProp = property.serializedObject.FindProperty(attr.boolName);
        if (boolProp == null)
        {
            EditorGUI.LabelField(position, label.text, "The field name on DisplayBasedOnBool cannot be found");
            return;
        }
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        EditorGUI.BeginProperty(position, label, property);
        if((boolProp.boolValue && attr.show) || (!boolProp.boolValue && !attr.show))
            EditorGUI.PropertyField(position, property, label, true);
        
        EditorGUI.EndProperty();
        EditorGUI.indentLevel = indent;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        DisplayBasedOnBoolAttribute attr = attribute as DisplayBasedOnBoolAttribute;
        var boolProp = property.serializedObject.FindProperty(attr.boolName);
        if (boolProp == null)
            return base.GetPropertyHeight(property,label);
        if((boolProp.boolValue && attr.show) || (!boolProp.boolValue && !attr.show))
            return base.GetPropertyHeight(property, label);
        return -UnityEditor.EditorGUIUtility.standardVerticalSpacing;
    }
}
