using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter {

    public class LargeMeals : MonoBehaviour, IMeal {

        public IReadOnlyCollection<Meal> Meals {
            get {
                return _meals.AsReadOnly();
            }
        }

        private List<Meal> _meals = new List<Meal>();

        private void Awake() {
            _meals.Add(new Meal("Bagel", "Bagel", 3, 53, 9, "Thomas everything bagel"));
            _meals.Add(new Meal("Ham", "Container", 16, 24, 72, "Hillshire honey ham family-sized"));
            _meals.Add(new Meal("Sliced Cheese", "Slice", 5, 0, 5, "Sargento provolone"));
            _meals.Add(new Meal("Hawaiian Roll", "Roll", 2.5f, 14, 3, "King's hawaiian savory butter rolls"));
        }
    }
}