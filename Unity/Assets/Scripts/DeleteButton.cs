using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter {
    public class DeleteButton : MonoBehaviour {

        public List<GameObject> RemovableGameObjects = new List<GameObject>();

        public Meal MealProportion;

        private MealTotals _mealTotals;

        public void DeleteMeal() {
            _mealTotals.SubtractMealProportion(MealProportion);
            foreach (var gameObject in RemovableGameObjects) {
                Destroy(gameObject);
            }
            Destroy(transform.parent.gameObject);
        }

        private void Start() {
            _mealTotals = FindObjectOfType<MealTotals>();
        }
    }
}
