using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ScriptableObjectSelectorAttribute))]
public class ScriptableObjectSelectorDrawer : PropertyDrawer
{
    private string[] assetPaths;
    private string[] assetNames;
    private int selectedAsset;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        if (property.propertyType == SerializedPropertyType.ObjectReference && (property.objectReferenceValue == null || (property.objectReferenceValue as ScriptableObject) != null))
        {
            ScriptableObjectSelectorAttribute selector = attribute as ScriptableObjectSelectorAttribute;
            LoadObjectsIfRequired(selector);
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUI.GetPropertyHeight(property)), property);
            if (assetNames != null && assetNames.Length > 0)
            {
                InitializeSelectedAsset(property);
                EditorGUI.indentLevel = 1;
                EditorGUI.BeginChangeCheck();
                selectedAsset = EditorGUI.Popup(new Rect(position.x, position.y + 15, position.width, 15), "Choose", selectedAsset, assetNames);
                if (EditorGUI.EndChangeCheck())
                {
                    if (selectedAsset == 0)
                        property.objectReferenceValue = null;
                    else
                        property.objectReferenceValue = AssetDatabase.LoadAssetAtPath(assetPaths[selectedAsset - 1], selector.type);
                }
            }
            if (property.objectReferenceValue != null)
            {
                //var e = Editor.CreateEditor(property.objectReferenceValue);
                //e.OnInspectorGUI();
            }
        }
        else
            EditorGUI.LabelField(position, label, "Use ScriptableObjectSelector on ScriptableObjects only");
        EditorGUI.indentLevel = indent;
    }

    private void InitializeSelectedAsset(SerializedProperty property)
    {
        if (property.objectReferenceValue == null)
        {
            selectedAsset = 0;
            return;
        }
        for (int i=0;i<assetPaths.Length;++i)
        {
            if(assetPaths[i] == AssetDatabase.GetAssetPath(property.objectReferenceValue))
            {
                selectedAsset = i + 1;
                return;
            }
        }

    }

    private void LoadObjectsIfRequired(ScriptableObjectSelectorAttribute selector)
    {
        if (assetNames == null || assetNames.Length == 0)
        {
            var guids = AssetDatabase.FindAssets("t:" + selector.type.Name, null);
            if (guids.Length == 0)
                return;
            assetPaths = new string[guids.Length];
            assetNames = new string[guids.Length + 1];
            assetNames[0] = "None";
            for (int i = 0; i < guids.Length; ++i)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                var index = assetPath.LastIndexOf("/");
                index++;
                var niceName = assetPath.Substring(index, assetPath.Length - index);
                niceName = niceName.Substring(0, niceName.Length - 6);
                assetPaths[i] = assetPath;
                assetNames[i + 1] = niceName;
            }
        }
    }

    private string GetAssetNiceName(string assetPath)
    {
        var index = assetPath.LastIndexOf("/");
        index++;
        var niceName = assetPath.Substring(index, assetPath.Length - index);
        niceName = niceName.Substring(0, niceName.Length - 6);
        return niceName;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property) * 2;
    }
}
