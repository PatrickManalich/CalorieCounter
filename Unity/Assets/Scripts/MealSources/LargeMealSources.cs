using System.Collections.Generic;

namespace CalorieCounter.MealSources {

    public class LargeMealSources : AbstractMealSources {

        private List<MealSource> _mealSources = new List<MealSource>();

        public override MealSourceType MealSourceType {
            get {
                return MealSourceType.Large;
            }
        }

        public override IReadOnlyList<MealSource> MealSources {
            get {
                return _mealSources.AsReadOnly();
            }
        }

        public LargeMealSources() {
            _mealSources.Add(new MealSource("Asparagus", "Ten Spears", 0.3f, 6.2f, 3.6f, "Ten spears of general asparagus", MealSourceType));
            _mealSources.Add(new MealSource("Bell Pepper", "Pepper", 0.2f, 7.6f, 1, "General green bell pepper", MealSourceType));
            _mealSources.Add(new MealSource("Bread-White", "Two Slices", 1.5f, 23, 6, "Two slices of Nature's Own whitewheat healthy white bread", MealSourceType));
            _mealSources.Add(new MealSource("Caesar Salad Kit", "Bag", 39, 30, 15, "Dole premium kit ultimate caesar", MealSourceType));
            _mealSources.Add(new MealSource("CFA Chicken Biscuit", "Biscuit", 20, 16, 48, "Chic-fil-A chicken biscuit", MealSourceType));
            _mealSources.Add(new MealSource("CFA Chicken Mini", "Mini", 3.5f, 9.25f, 4.8f, "Chic-fil-A chicken mini", MealSourceType));
            _mealSources.Add(new MealSource("CFA Chicken Sandwich", "Sandwich", 19, 40, 28, "Chic-fil-A chicken sandwich", MealSourceType));
            _mealSources.Add(new MealSource("CFA Fries-Large", "Container", 24, 56, 6, "Chic-fil-A large fries", MealSourceType));
            _mealSources.Add(new MealSource("CFA Fries-Medium", "Container", 18, 43, 5, "Chic-fil-A medium fries", MealSourceType));
            _mealSources.Add(new MealSource("CFA Hashbrowns", "Container", 17, 23, 3, "Chic-fil-A hashbrowns", MealSourceType));
            _mealSources.Add(new MealSource("CFA Nuggets-12 Count", "Container", 18, 13.5f, 42, "Chic-fil-A twelve count nuggets", MealSourceType));
            _mealSources.Add(new MealSource("CFA Shake-Large", "Shake", 25, 86, 15, "Chic-fil-A large vanilla shake", MealSourceType));
            _mealSources.Add(new MealSource("CFA Sprite-Large", "Container", 0, 65, 0, "Chic-fil-A large sprite", MealSourceType));
            _mealSources.Add(new MealSource("CFA Sprite-Medium", "Container", 0, 44, 0, "Chic-fil-A medium sprite", MealSourceType));
            _mealSources.Add(new MealSource("Cheese-Shredded", "Half Cup", 16, 2, 14, "Kroger queso quesadilla finely shredded cheese", MealSourceType));
            _mealSources.Add(new MealSource("Cheese-Sliced", "Slice", 5, 0, 5, "Sargento provolone cheese", MealSourceType));
            _mealSources.Add(new MealSource("Chicken Breast", "Breast", 1, 0, 25, "Simple Truth natural chicken breast", MealSourceType));
            _mealSources.Add(new MealSource("Egg", "Egg", 4.8f, 0.4f, 6.3f, "General egg", MealSourceType));
            _mealSources.Add(new MealSource("FH Chips", "Bag", 8, 20, 2, "Firehouse Funyuns onion flavored rings", MealSourceType));
            _mealSources.Add(new MealSource("FH Powerade-Medium", "Cup", 0, 35, 0, "Firehouse Powerade sports drink orange 20 fl oz", MealSourceType));
            _mealSources.Add(new MealSource("FH Roast Beef-Medium", "Sandwich", 14, 44, 39, "Firehouse medium roast beef sandwich on white bread with only provolone cheese", MealSourceType));
            _mealSources.Add(new MealSource("Garlic Knots", "Knot", 4, 19, 3, "New York Bakery garlic knot with real garlic", MealSourceType));
            _mealSources.Add(new MealSource("Ground Beef-85/15", "Container", 68, 0, 84, "Kroger ground beef 85% lean meat and 15% fat", MealSourceType));
            _mealSources.Add(new MealSource("Ham", "Bag", 12.6f, 2.7f, 55.8f, "Kroger bag of 0.8 lb of sliced ham", MealSourceType));
            _mealSources.Add(new MealSource("Hawaiian Roll", "Roll", 2.5f, 14, 3, "King's Hawaiian savory butter roll", MealSourceType));
            _mealSources.Add(new MealSource("Pasta", "Box", 8, 336, 56, "Barilla farfalle", MealSourceType));
            _mealSources.Add(new MealSource("Pasta Sauce", "Jar", 7.5f, 65, 10, "Prego italian sauce traditional 24 oz jar", MealSourceType));
            _mealSources.Add(new MealSource("Peas and Carrots", "Half Cup", 0.3f, 8.1f, 2.5f, "General peas and carrots", MealSourceType));
            _mealSources.Add(new MealSource("Pizza-Pepperoni", "Pizza", 100, 120, 70, "Screamin' Sicilian holy pepperoni pizza", MealSourceType));
            _mealSources.Add(new MealSource("Pork Loin", "Roast", 80, 0, 234.7f, "Simple Truth natural pork 32 oz", MealSourceType));
            _mealSources.Add(new MealSource("Rice-White", "Cup", 0, 36, 3, "Mahatma extra long enriched rice", MealSourceType));
            _mealSources.Add(new MealSource("Texas Toast", "Slice", 9, 17, 3, "New York Bakery texas toast with real garlic", MealSourceType));
            _mealSources.Add(new MealSource("Tomato Sauce", "Can", 4, 0, 1, "Goya tomato sauce spanish style", MealSourceType));
            _mealSources.Add(new MealSource("Tortilla", "Tortilla", 3, 26, 4, "Mission flour tortillas soft taco", MealSourceType));
            _mealSources.Add(new MealSource("Turkey", "Bag", 7.6f, 19.8f, 34.6f, "Kroger bag of 0.8 lb of sliced turkey", MealSourceType));
        }
    }
}