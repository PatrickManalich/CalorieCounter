using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter {

    public class SmallMeals : AbstractMeals {

        private List<Meal> _meals = new List<Meal>();

        public override IReadOnlyCollection<Meal> Meals {
            get {
                return _meals.AsReadOnly();
            }
        }

        public SmallMeals() {
            _meals.Add(new Meal("Bagel", "Bagel", 3, 53, 9, "Thomas everything bagel"));
            _meals.Add(new Meal("Apple", "Fruit", 0.3f, 25, 0.5f, "Common apple"));
            _meals.Add(new Meal("Banana", "Fruit", 0.4f, 27, 1.3f, "Common banana"));
        }
    }
}
