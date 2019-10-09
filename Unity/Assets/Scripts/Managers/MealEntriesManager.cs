using CalorieCounter.MealEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class MealEntriesManager : MonoBehaviour {

        private SortedList<DateTime, MealEntry> _mealEntries = new SortedList<DateTime, MealEntry>();

        public MealEntry ImportMealEntry(DateTime dateTime) {
            if (!_mealEntries.ContainsKey(dateTime)) {
                var mealEntry = JsonConverter.ImportFile<MealEntry>(JsonConverter.GetMealEntryPath(dateTime));
                _mealEntries.Add(dateTime, mealEntry);
            }
            return _mealEntries[dateTime];
        }

        public void ExportMealEntry(MealEntry mealEntry, DateTime dateTime) {
            JsonConverter.ExportFile(mealEntry, JsonConverter.GetMealEntryPath(dateTime));
            _mealEntries.Remove(dateTime);
            ImportMealEntry(dateTime);
        }
    }
}
