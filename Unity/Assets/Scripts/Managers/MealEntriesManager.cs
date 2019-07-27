using CalorieCounter.MealEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class MealEntriesManager : MonoBehaviour {

        private SortedList<DateTime, MealEntry> _importedMealEntries = new SortedList<DateTime, MealEntry>();

        public MealEntry ImportMealEntry(DateTime dateTime) {
            if (!_importedMealEntries.ContainsKey(dateTime)) {
                MealEntry importedMealEntry = JsonConverter.Import<MealEntry>(GetMealEntryPath(dateTime));
                _importedMealEntries.Add(dateTime, importedMealEntry);
            }
            return _importedMealEntries[dateTime];
        }

        public void ExportMealEntry(MealEntry mealEntry, DateTime dateTime) {
            JsonConverter.Export(mealEntry, GetMealEntryPath(dateTime));
            _importedMealEntries.Remove(dateTime);
            ImportMealEntry(dateTime);
        }

        private string GetMealEntryPath(DateTime dateTime) {
            string mealEntryFileDate = "-" + dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day;
            string mealEntryFileName = GlobalPaths.MealEntryFilePrefix + mealEntryFileDate + GlobalPaths.MealEntryFileExtension;
            return Path.Combine(GlobalPaths.MealEntriesDir, mealEntryFileName);
        }
    }
}
