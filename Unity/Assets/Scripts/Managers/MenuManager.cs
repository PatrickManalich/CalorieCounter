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
                key.GetComponent<Button>().onClick.AddListener(() => OnSceneButtonClicked(key));
            _quitButton.onClick.AddListener(() => OnQuitButtonClicked());
            _versionText.text = Application.version;
        }

        private void Update() {
            if (Input.GetKeyDown(_menuKeyCode)) {
                _menuCanvas.SetActive(!_menuCanvas.activeSelf);
            }
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
    }
}