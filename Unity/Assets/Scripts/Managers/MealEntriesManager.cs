using CalorieCounter.MealEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class MealEntriesManager : MonoBehaviour {

        private SortedList<DateTime, MealEntry> _mealEntries = new SortedList<DateTime, MealEntry>();

        public MealEntry ImportMealEntry(DateTime dateTime) {
            if (!_mealEntries.ContainsKey(dateTime)) {
                MealEntry mealEntry = JsonConverter.Import<MealEntry>(GetMealEntryPath(dateTime));
                _mealEntries.Add(dateTime, mealEntry);
            }
            return _mealEntries[dateTime];
        }

        public void ExportMealEntry(MealEntry mealEntry, DateTime dateTime) {
            JsonConverter.Export(mealEntry, GetMealEntryPath(dateTime));
            _mealEntries.Remove(dateTime);
            ImportMealEntry(dateTime);
        }

        private string GetMealEntryPath(DateTime dateTime) {
            string mealEntryFileDate = "-" + dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day;
            string mealEntryFileName = GlobalPaths.MealEntryFilePrefix + mealEntryFileDate + GlobalPaths.JsonFileExtension;
            return Path.Combine(GlobalPaths.MealEntriesDirectoryName, mealEntryFileName);
        }
    }
}
