using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries {
    public class DeleteButton : MonoBehaviour {

        public List<GameObject> RemovableGameObjects = new List<GameObject>();

        public MealProportion MealProportion;

        private MealEntryHandler _mealEntryHandler;

        public void DeleteMeal() {
            _mealEntryHandler.SubtractMealProportion(MealProportion);
            foreach (var gameObject in RemovableGameObjects) {
                Destroy(gameObject);
            }
            Destroy(transform.parent.gameObject);
        }

        private void Start() {
            _mealEntryHandler = FindObjectOfType<MealEntryHandler>();
        }
    }
}
