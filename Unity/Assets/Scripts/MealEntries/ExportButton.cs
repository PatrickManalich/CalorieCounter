using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private MealEntryHandler _mealEntryHandler = default;

        public void TryExporting() {
            _mealEntryHandler.ExportMealEntry();
            gameObject.SetActive(false);
        }
    }
}
