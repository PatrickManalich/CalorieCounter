using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries {
    public class DeleteButton : MonoBehaviour {

        public List<GameObject> RemovableGameObjects = new List<GameObject>();

        public MealProportion MealProportion;

        private MealEntryTracker _mealEntryTracker;

        public void DeleteMeal() {
            _mealEntryTracker.SubtractMealProportion(MealProportion);
            foreach (var gameObject in RemovableGameObjects) {
                Destroy(gameObject);
            }
            Destroy(transform.parent.gameObject);
        }

        private void Start() {
            _mealEntryTracker = FindObjectOfType<MealEntryTracker>();
        }
    }
}
