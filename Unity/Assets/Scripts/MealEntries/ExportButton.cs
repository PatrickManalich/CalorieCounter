using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private MealEntryHandler _mealEntryHandler = default;

        public void TryExporting() {
            JsonUtility.Export(_mealEntryHandler.GetMealEntry(), _mealEntryHandler.GetMealEntryPath());
            gameObject.SetActive(false);
        }
    }
}
