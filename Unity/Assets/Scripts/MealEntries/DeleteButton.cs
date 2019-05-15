using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries {
    public class DeleteButton : MonoBehaviour {
        public MealProportion MealProportion;

        public AbstractScrollView ScrollView;

        private MealEntryHandler _mealEntryHandler;

        public void DeleteMeal() {
            _mealEntryHandler.SubtractMealProportion(MealProportion);
            ScrollView.SubtractMealProportion(MealProportion);
        }

        private void Start() {
            _mealEntryHandler = FindObjectOfType<MealEntryHandler>();
        }
    }
}
