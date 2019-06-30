using UnityEngine;

namespace CalorieCounter.Managers {

    public class GameManager : MonoBehaviour {

        public static GameManager Instance { get; private set; }

        public static CustomSceneManager CustomSceneManager { get { return Instance._customSceneManager; } }

        public static MenuManager MenuManager { get { return Instance._menuManager; } }

        public static TargetEntriesManager TargetEntriesManager { get { return Instance._targetEntriesManager; } }

        public static ScaleEntriesManager ScaleEntriesManager { get { return Instance._scaleEntriesManager; } }

        [SerializeField]
        private CustomSceneManager _customSceneManager = default;

        [SerializeField]
        private MenuManager _menuManager = default;

        [SerializeField]
        private TargetEntriesManager _targetEntriesManager = default;

        [SerializeField]
        private ScaleEntriesManager _scaleEntriesManager = default;

        private void Awake() {
            if (Instance == null) {
                Instance = this as GameManager;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }

            if (CustomSceneManager.CurrentScene == Scene.Menu) {
                MenuManager.ShowMenu();
            }
        }

    }
}