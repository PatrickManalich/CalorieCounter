using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private MealEntryTracker _mealEntryTracker = default;

        private const string mealEntriesFileName = @"MealEntries.json";

        public void TryExporting() {
            JsonUtility.ExportEntry(_mealEntryTracker.CurrentMealEntry, mealEntriesFileName);
            gameObject.SetActive(false);
        }
    }
}
