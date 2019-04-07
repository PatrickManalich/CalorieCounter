using System.Collections.Generic;

namespace CalorieCounter.Meals {

    public class LargeMeals : AbstractMeals {

        private List<Meal> _meals = new List<Meal>();

        public override IReadOnlyList<Meal> Meals {
            get {
                return _meals.AsReadOnly();
            }
        }

        public LargeMeals() {
            _meals.Add(new Meal("Ham", "Container", 16, 24, 72, "Hillshire honey ham family-sized"));
            _meals.Add(new Meal("Sliced Cheese", "Slice", 5, 0, 5, "Sargento provolone"));
            _meals.Add(new Meal("Hawaiian Roll", "Roll", 2.5f, 14, 3, "King's hawaiian savory butter rolls"));
        }
    }
}