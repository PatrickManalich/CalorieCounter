using CalorieCounter.Globals;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public class SmallMeals : AbstractMeals {

        private List<MealSource> _meals = new List<MealSource>();

        public override MealTypes MealType {
            get {
                return MealTypes.Small;
            }
        }

        public override IReadOnlyList<MealSource> Meals {
            get {
                return _meals.AsReadOnly();
            }
        }

        public SmallMeals() {
            _meals.Add(new MealSource("Bagel", "Bagel", 3, 53, 9, "Thomas everything bagel", MealType));
            _meals.Add(new MealSource("Apple", "Fruit", 0.3f, 25, 0.5f, "Common apple", MealType));
            _meals.Add(new MealSource("Banana", "Fruit", 0.4f, 27, 1.3f, "Common banana", MealType));
        }
    }
}
