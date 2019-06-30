using UnityEngine;

namespace CalorieCounter.Managers {

    public class MenuManager : MonoBehaviour {

        [SerializeField]
        private GameObject _menuCanvas = default;

        private KeyCode _menuKeyCode = KeyCode.Escape;

        public void HideMenu() {
            _menuCanvas.SetActive(false);
        }

        private void Update() {
            if (Input.GetKeyDown(_menuKeyCode)) {
                _menuCanvas.SetActive(!_menuCanvas.activeSelf);
            }
        }
    }
}