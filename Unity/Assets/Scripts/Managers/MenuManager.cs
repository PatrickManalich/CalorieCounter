using RotaryHeart.Lib.SerializableDictionary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.Managers {

    public class MenuManager : MonoBehaviour {

        [System.Serializable]
        private class SceneButtonDictionary : SerializableDictionaryBase<GameObject, Scene> { }

        [SerializeField]
        private GameObject _menuCanvas = default;

        [SerializeField]
        private TextMeshProUGUI _versionText = default;

        [SerializeField]
        private SceneButtonDictionary _sceneButtonDictionary = default;

        private static readonly KeyCode _menuKeyCode = KeyCode.Escape;

        public void ShowMenu() {
            _menuCanvas.SetActive(true);
        }

        public void HideMenu() {
            _menuCanvas.SetActive(false);
        }

        private void Start()
        {
            foreach (var key in _sceneButtonDictionary.Keys)
            {
                var button = key.GetComponent<Button>();
                button.onClick.AddListener(() => GameManager.CustomSceneManager.LoadScene(_sceneButtonDictionary[key]));
                button.onClick.AddListener(() => GameManager.MenuManager.HideMenu());
            }
            _versionText.text = Application.version;
        }

        private void Update() {
            if (Input.GetKeyDown(_menuKeyCode)) {
                _menuCanvas.SetActive(!_menuCanvas.activeSelf);
            }
        }
    }
}