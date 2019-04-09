using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Meals {
    public class DeleteButton : MonoBehaviour {

        public List<GameObject> RemovableGameObjects = new List<GameObject>();

        public Meal MealProportion;

        private TotalMeal _totalMeal;

        public void DeleteMeal() {
            _totalMeal.SubtractMealProportion(MealProportion);
            foreach (var gameObject in RemovableGameObjects) {
                Destroy(gameObject);
            }
            Destroy(transform.parent.gameObject);
        }

        private void Start() {
            _totalMeal = FindObjectOfType<TotalMeal>();
        }
    }
}
