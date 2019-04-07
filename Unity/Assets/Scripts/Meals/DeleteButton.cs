using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Meals {
    public class DeleteButton : MonoBehaviour {

        public List<GameObject> RemovableGameObjects = new List<GameObject>();

        public Meal MealProportion;

        private Totals _totals;

        public void DeleteMeal() {
            _totals.SubtractMealProportion(MealProportion);
            foreach (var gameObject in RemovableGameObjects) {
                Destroy(gameObject);
            }
            Destroy(transform.parent.gameObject);
        }

        private void Start() {
            _totals = FindObjectOfType<Totals>();
        }
    }
}
