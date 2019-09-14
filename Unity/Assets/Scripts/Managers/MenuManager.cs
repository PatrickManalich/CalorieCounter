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
        private Button _quitButton = default;

        [SerializeField]
        private SceneButtonDictionary _sceneButtonDictionary = default;

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
                key.GetComponent<Button>().onClick.AddListener(() => OnSceneButtonClicked(key));
            }
            _quitButton.onClick.AddListener(() => OnQuitButtonClicked());
            GameManager.InputKeyManager.InputKeyPressed += OnInputKeyPressed;
            _versionText.text = Application.version;
        }

        private void OnDestroy()
        {
            GameManager.InputKeyManager.InputKeyPressed -= OnInputKeyPressed;
        }

        private void OnSceneButtonClicked(GameObject key)
        {
            GameManager.CustomSceneManager.LoadScene(_sceneButtonDictionary[key]);
            GameManager.MenuManager.HideMenu();
        }

        private void OnQuitButtonClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }

        private void OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
        {
            if (e.InputKeyCode == InputKeyCode.ToggleMenu)
            {
                _menuCanvas.SetActive(!_menuCanvas.activeSelf);
            }
        }
    }
}