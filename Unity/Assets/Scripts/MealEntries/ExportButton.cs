﻿using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private MealEntryTracker _mealEntryTracker = default;

        public void TryExporting() {
            JsonUtility.Export(_mealEntryTracker.CurrentMealEntry, _mealEntryTracker.GetCurrentMealEntryPath());
            gameObject.SetActive(false);
        }
    }
}