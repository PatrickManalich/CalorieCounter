using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DisplayBasedOnEnumAttribute))]
public class DisplayBasedOnEnumPropertyDrawer : PropertyDrawer
{



    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DisplayBasedOnEnumAttribute attr = attribute as DisplayBasedOnEnumAttribute;
        var enumProp = property.serializedObject.FindProperty(attr.enumName);
        if(enumProp == null)
        {
            EditorGUI.LabelField(position, label.text, "The field name on DispalyBasedOnEnum cannot be found");
            return;
        }
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        EditorGUI.BeginProperty(position, label, property);
        if((enumProp.enumValueIndex == attr.enumIndex && attr.show) || (enumProp.enumValueIndex != attr.enumIndex  && !attr.show))
            EditorGUI.PropertyField(position, property, label, true);
        
        EditorGUI.EndProperty();
        EditorGUI.indentLevel = indent;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        DisplayBasedOnEnumAttribute attr = attribute as DisplayBasedOnEnumAttribute;
        var enumProp = property.serializedObject.FindProperty(attr.enumName);
        if(enumProp == null)
        {
            return base.GetPropertyHeight(property, label);
        }
        if ((enumProp.enumValueIndex == attr.enumIndex && attr.show) || (enumProp.enumValueIndex != attr.enumIndex && !attr.show))
            return base.GetPropertyHeight(property, label);
        return -UnityEditor.EditorGUIUtility.standardVerticalSpacing;
    }
}
