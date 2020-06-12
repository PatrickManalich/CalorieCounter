using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    public class TextPrefabConverter : EditorWindow
    {
        private GameObject _targetTextGameObject;
        private GameObject _prefabTextGameObject;

        [MenuItem("Calorie Counter/Window/Text Prefab Converter")]
        public static void ShowWindow()
        {
            GetWindow<TextPrefabConverter>("Text Prefab Converter");
        }

        public void OnGUI()
        {
            EditorGUIUtility.labelWidth = 100;

            _targetTextGameObject = (GameObject)EditorGUILayout.ObjectField("Target Text: ", _targetTextGameObject, typeof(GameObject), true);
            _prefabTextGameObject = (GameObject)EditorGUILayout.ObjectField("Prefab Text: ", _prefabTextGameObject, typeof(GameObject), true);

            if (GUILayout.Button("Convert Target Text Into Prefab Text"))
            {
                if (_targetTextGameObject == null  || _prefabTextGameObject == null)
                {
                    Debug.LogWarning("Both GameObjects must be referenced");
                    return;
                }
                var targetText = _targetTextGameObject.GetComponent<TextMeshProUGUI>();
                var prefabText = _prefabTextGameObject.GetComponent<TextMeshProUGUI>();
                if (targetText == null || prefabText == null)
                {
                    Debug.LogWarning("Both GameObjects must have TextMeshProUGUI components");
                    return;
                }
                var targetTextScene = _targetTextGameObject.scene;
                var prefabTextScene = _prefabTextGameObject.scene;
                if(targetTextScene.rootCount == 0 || prefabTextScene.rootCount != 0)
                {
                    Debug.LogWarning("Make sure Target Text is in a scene and Prefab Text is a prefab");
                    return;
                }

                var prefabInstanceGameObject = (GameObject)PrefabUtility.InstantiatePrefab(_prefabTextGameObject, _targetTextGameObject.transform.parent);
                prefabInstanceGameObject.transform.SetSiblingIndex(_targetTextGameObject.transform.GetSiblingIndex());
                prefabInstanceGameObject.name = _targetTextGameObject.name;

                UnityEditorInternal.ComponentUtility.CopyComponent(_targetTextGameObject.GetComponent<RectTransform>());
                UnityEditorInternal.ComponentUtility.PasteComponentValues(prefabInstanceGameObject.GetComponent<RectTransform>());

                var prefabInstanceText = prefabInstanceGameObject.GetComponent<TextMeshProUGUI>();
                prefabInstanceText.text = targetText.text;
                prefabInstanceText.alignment = targetText.alignment;

                DestroyImmediate(_targetTextGameObject);
                EditorSceneManager.MarkSceneDirty(targetTextScene);
                Debug.Log("Target text converted to prefab text");
            }
        }
    }
}
