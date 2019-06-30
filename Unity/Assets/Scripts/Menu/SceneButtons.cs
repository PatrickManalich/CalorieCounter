using CalorieCounter.Managers;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.Menu {

    public class SceneButtons: MonoBehaviour {

        [System.Serializable]
        private class SceneButtonDictionary : SerializableDictionaryBase<GameObject, Scene> { }

        [SerializeField]
        private SceneButtonDictionary _sceneButtonDictionary = default;

        private void Start() {
            foreach (var key in _sceneButtonDictionary.Keys) {
                key.GetComponent<Button>().onClick.AddListener(() => GameManager.CustomSceneManager.LoadScene(_sceneButtonDictionary[key]));
            }
        }
    }
}
