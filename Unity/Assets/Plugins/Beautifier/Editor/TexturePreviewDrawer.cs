using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TexturePreviewAttribute))]
public class TexturePreviewDrawer : PropertyDrawer
{
    private Texture t;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        if (property.propertyType == SerializedPropertyType.ObjectReference && ((property.objectReferenceValue as Texture) != null || property.objectReferenceValue == null))
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, 15), property);
            if (property.objectReferenceValue != null)
            {
                EditorGUI.BeginChangeCheck();
                t = property.objectReferenceValue as Texture;
                if (EditorGUI.EndChangeCheck())
                {
                    property.objectReferenceValue = t;
                }
                EditorGUI.DrawPreviewTexture(new Rect(position.x+30,position.y+20,80,80), property.objectReferenceValue as Texture);
            }
            EditorGUI.EndProperty();
        }
        else
            EditorGUI.LabelField(position, label.text, "Use TexturePreview on textures.");
        EditorGUI.indentLevel = indent;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.objectReferenceValue != null)
            return EditorGUI.GetPropertyHeight(property) + 90;
        return EditorGUI.GetPropertyHeight(property);
    }
}
