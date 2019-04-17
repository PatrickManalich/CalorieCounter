using System.IO;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private MealEntryTracker _mealEntryTracker = default;

        private const string MealEntriesDir = @"MealEntries";
        private const string MealEntryFilePrefix = @"MealEntry";
        private const string MealEntryFileExtension = @".json";


        public void TryExporting() {
            MealEntry currentMealEntry = _mealEntryTracker.CurrentMealEntry;
            string mealEntryFileDate = "-" + currentMealEntry.Date.Year + "-" + currentMealEntry.Date.Month + "-" + currentMealEntry.Date.Day;
            string mealEntryFileName = MealEntryFilePrefix + mealEntryFileDate + MealEntryFileExtension;
            JsonUtility.Export(_mealEntryTracker.CurrentMealEntry, Path.Combine(MealEntriesDir, mealEntryFileName));
            gameObject.SetActive(false);
        }
    }
}
