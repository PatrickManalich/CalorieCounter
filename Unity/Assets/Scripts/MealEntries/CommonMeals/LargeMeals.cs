using CalorieCounter.Globals;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries.CommonMeals {

    public class LargeMeals : AbstractMeals {

        private List<MealSource> _mealSources = new List<MealSource>();

        public override MealType MealType {
            get {
                return MealType.Large;
            }
        }

        public override IReadOnlyList<MealSource> MealSources {
            get {
                return _mealSources.AsReadOnly();
            }
        }

        public LargeMeals() {
            _mealSources.Add(new MealSource("Ham", "Container", 16, 24, 72, "Hillshire honey ham family-sized", MealType));
            _mealSources.Add(new MealSource("Sliced Cheese", "Slice", 5, 0, 5, "Sargento provolone", MealType));
            _mealSources.Add(new MealSource("Hawaiian Roll", "Roll", 2.5f, 14, 3, "King's hawaiian savory butter rolls", MealType));
        }
    }
}