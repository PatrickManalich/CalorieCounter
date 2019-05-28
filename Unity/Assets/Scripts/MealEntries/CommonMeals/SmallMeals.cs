using CalorieCounter.Globals;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries.CommonMeals {

    public class SmallMeals : AbstractMeals {

        private List<MealSource> _mealSources = new List<MealSource>();

        public override MealType MealType {
            get {
                return MealType.Small;
            }
        }

        public override IReadOnlyList<MealSource> MealSources {
            get {
                return _mealSources.AsReadOnly();
            }
        }

        public SmallMeals() {
            _mealSources.Add(new MealSource("Bagel", "Bagel", 3, 53, 9, "Thomas everything bagel", MealType));
            _mealSources.Add(new MealSource("Apple", "Fruit", 0.3f, 25, 0.5f, "Common apple", MealType));
            _mealSources.Add(new MealSource("Banana", "Fruit", 0.4f, 27, 1.3f, "Common banana", MealType));
        }
    }
}
