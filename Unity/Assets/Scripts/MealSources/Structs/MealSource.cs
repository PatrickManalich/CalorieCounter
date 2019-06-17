using System;

namespace CalorieCounter.MealSources {

    public struct MealSource : IComparable<MealSource> {

        public string Name;
        public string ServingSize;
        public float Fat;
        public float Carbs;
        public float Protein;
        public float Calories;
        public string Description;
        public MealSourceType MealSourceType;

        public MealSource(string name, string servingSize, float fat, float carbs, float protein, string description, MealSourceType mealSourceType) {
            Name = name;
            ServingSize = servingSize;
            Fat = fat > 0 ? GlobalMethods.Round(fat) : 0;
            Carbs = carbs > 0 ? GlobalMethods.Round(carbs) : 0;
            Protein = protein > 0 ? GlobalMethods.Round(protein) : 0;
            Calories = GlobalMethods.Round((fat * 9) + (carbs * 4) + (protein * 4));
            Description = description;
            MealSourceType = mealSourceType;
        }

        public static MealSource CreateCustomMealSource(float fat, float carbs, float protein) {
            return new MealSource("Custom Meal", "Meal", fat, carbs, protein, "", MealSourceType.Custom);
        }

        public static bool operator ==(MealSource meal1, MealSource meal2) {
            return meal1.Equals(meal2);
        }

        public static bool operator !=(MealSource meal1, MealSource meal2) {
            return !meal1.Equals(meal2);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return string.Format("Name: {0}, Serving Size: {1}, [ Fat: {2}, Carbs: {3}, Protein: {4}, Calories: {5} ], Description: {6}, Meal Type: {7}",
                Name, ServingSize , Fat, Carbs, Protein, Calories, Description, MealSourceType);
        }

        public int CompareTo(MealSource mealSource) {
            return string.Compare(Name, mealSource.Name);
        }
    }
}
