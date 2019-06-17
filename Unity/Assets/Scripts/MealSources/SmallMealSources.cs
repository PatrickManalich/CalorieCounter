using System.Collections.Generic;

namespace CalorieCounter.MealSources {

    public class SmallMealSources : AbstractMealSources {

        private List<MealSource> _mealSources = new List<MealSource>();

        public override MealSourceType MealSourceType {
            get {
                return MealSourceType.Small;
            }
        }

        public override IReadOnlyList<MealSource> MealSources {
            get {
                return _mealSources.AsReadOnly();
            }
        }

        public SmallMealSources() {
            _mealSources.Add(new MealSource("Apple", "Fruit", 0.3f, 25, 0.5f, "General apple", MealSourceType));
            _mealSources.Add(new MealSource("Bacon", "Strip", 4, 0.2f, 4, "General bacon", MealSourceType));
            _mealSources.Add(new MealSource("Banana", "Fruit", 0.4f, 27, 1.3f, "General banana", MealSourceType));
            _mealSources.Add(new MealSource("Bagel", "Bagel", 3.5f, 53, 9, "Thomas everything bagel", MealSourceType));
            _mealSources.Add(new MealSource("Bread-White", "Two Slices", 1.5f, 23, 6, "Two slices of Nature's Own whitewheat healthy white bread", MealSourceType));
            _mealSources.Add(new MealSource("Cream Cheese", "Bagel", 20, 3.2f, 3.6f, "Philadelphia cream cheese original", MealSourceType));
            _mealSources.Add(new MealSource("Egg", "Egg", 4.8f, 0.4f, 6.3f, "General egg", MealSourceType));
            _mealSources.Add(new MealSource("Jelly", "Sandwich", 0, 26, 0, "Two tbsp. of Welch's concord grape jelly", MealSourceType));
            _mealSources.Add(new MealSource("Peanut Butter", "Sandwich", 16, 8, 7, "Two tbsp. of Jif creamy peanut butter", MealSourceType));
            _mealSources.Add(new MealSource("Protein Bar", "Bar", 20, 9, 30, "Clif builder's protein bar chocolate mint", MealSourceType));
            _mealSources.Add(new MealSource("Protein Smoothie", "Smoothie", 5.4f, 60.4f, 32.2f, "", MealSourceType));
        }
    }
}
