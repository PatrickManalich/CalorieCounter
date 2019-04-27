using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public class LargeMeals : AbstractMeals {

        private List<MealSource> _meals = new List<MealSource>();

        public override MealTypes MealType {
            get {
                return MealTypes.Large;
            }
        }

        public override IReadOnlyList<MealSource> Meals {
            get {
                return _meals.AsReadOnly();
            }
        }

        public LargeMeals() {
            _meals.Add(new MealSource("Ham", "Container", 16, 24, 72, "Hillshire honey ham family-sized", MealType));
            _meals.Add(new MealSource("Sliced Cheese", "Slice", 5, 0, 5, "Sargento provolone", MealType));
            _meals.Add(new MealSource("Hawaiian Roll", "Roll", 2.5f, 14, 3, "King's hawaiian savory butter rolls", MealType));
        }
    }
}