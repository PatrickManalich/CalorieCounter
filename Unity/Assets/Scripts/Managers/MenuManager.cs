using RotaryHeart.Lib.SerializableDictionary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CalorieCounter.Managers {

    public class MenuManager : MonoBehaviour {

        // Can't use Button instead of GameObject because Serializable Dictionary plugin doesn't support them
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
            foreach (var sceneButton in _sceneButtonDictionary.Keys)
            {
                sceneButton.GetComponent<Button>().onClick.AddListener(SceneButton_OnClick);
            }
            _quitButton.onClick.AddListener(QuitButton_OnClick);
            GameManager.InputKeyManager.InputKeyPressed += InputKeyManager_OnInputKeyPressed;
            _versionText.text = Application.version;
        }

        private void OnDestroy()
        {
            GameManager.InputKeyManager.InputKeyPressed -= InputKeyManager_OnInputKeyPressed;
            _quitButton.onClick.RemoveListener(QuitButton_OnClick);
            foreach (var sceneButton in _sceneButtonDictionary.Keys)
            {
                sceneButton.GetComponent<Button>().onClick.RemoveListener(SceneButton_OnClick);
            }
        }

        private void SceneButton_OnClick()
        {
            GameManager.CustomSceneManager.LoadScene(_sceneButtonDictionary[EventSystem.current.currentSelectedGameObject]);
            GameManager.MenuManager.HideMenu();
        }

        private void QuitButton_OnClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }

        private void InputKeyManager_OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
        {
            if (e.InputKeyCode == InputKeyCode.ToggleMenu && GameManager.CustomSceneManager.CurrentScene != Scene.Menu)
            {
                _menuCanvas.SetActive(!_menuCanvas.activeSelf);
            }
        }
    }
}