using UnityEngine;

namespace CalorieCounter.Managers {

    public class GameManager : MonoBehaviour {

        public static GameManager Instance { get; private set; }

        public static CustomSceneManager CustomSceneManager { get { return Instance._customSceneManager; } }

        public static MenuManager MenuManager { get { return Instance._menuManager; } }

        public static TargetEntriesManager TargetEntriesManager { get { return Instance._targetEntriesManager; } }

        [SerializeField]
        private CustomSceneManager _customSceneManager = default;

        [SerializeField]
        private MenuManager _menuManager = default;

        [SerializeField]
        private TargetEntriesManager _targetEntriesManager = default;

        private void Awake() {
            if (Instance == null) {
                Instance = this as GameManager;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }

            if (CustomSceneManager.CurrentScene != Scene.Menu) {
                MenuManager.HideMenu();
            }
        }

    }
}