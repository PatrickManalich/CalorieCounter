using UnityEngine;

namespace CalorieCounter.Managers {

    public class GameManager : MonoBehaviour {

        public static GameManager Instance { get; private set; }

        public static CustomSceneManager CustomSceneManager { get { return Instance._customSceneManager; } }

        public static MenuManager MenuManager { get { return Instance._menuManager; } }

        public static TargetEntriesManager TargetEntriesManager { get { return Instance._targetEntriesManager; } }

        public static ScaleEntriesManager ScaleEntriesManager { get { return Instance._scaleEntriesManager; } }

        public static MealEntriesManager MealEntriesManager { get { return Instance._mealEntriesManager; } }

        public static MealSourcesManager MealSourcesManager { get { return Instance._mealSourcesManager; } }

        public static LogMessageManager LogMessageManager { get { return Instance._logMessageManager; } }

        public static InputKeyManager InputKeyManager { get { return Instance._inputKeyManager; } }

        public static BackgroundMusicManager BackgroundMusicManager { get { return Instance._backgroundMusicManager; } }

        public static SoundEffectsManager SoundEffectsManager { get { return Instance._soundEffectsManager; } }

        [SerializeField]
        private CustomSceneManager _customSceneManager = default;

        [SerializeField]
        private MenuManager _menuManager = default;

        [SerializeField]
        private TargetEntriesManager _targetEntriesManager = default;

        [SerializeField]
        private ScaleEntriesManager _scaleEntriesManager = default;

        [SerializeField]
        private MealEntriesManager _mealEntriesManager = default;

        [SerializeField]
        private MealSourcesManager _mealSourcesManager = default;

        [SerializeField]
        private LogMessageManager _logMessageManager = default;

        [SerializeField]
        private InputKeyManager _inputKeyManager = default;

        [SerializeField]
        private BackgroundMusicManager _backgroundMusicManager = default;

        [SerializeField]
        private SoundEffectsManager _soundEffectsManager = default;

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