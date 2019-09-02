
namespace CalorieCounter.MealSources {

    public struct MealSource {

        public string Id;
        public string Name;
        public string ServingSize;
        public float Fat;
        public float Carbs;
        public float Protein;
        public float Calories;
        public string Description;
        public MealSourceType MealSourceType;
        public bool Archived;

        public MealSource(int mealSourcesCount, string name, string servingSize, float fat, float carbs, float protein, string description, MealSourceType mealSourceType) {
            Id = mealSourceType.ToString() + mealSourcesCount;
            Name = name;
            ServingSize = servingSize;
            Fat = fat > 0 ? GlobalMethods.Round(fat) : 0;
            Carbs = carbs > 0 ? GlobalMethods.Round(carbs) : 0;
            Protein = protein > 0 ? GlobalMethods.Round(protein) : 0;
            Calories = GlobalMethods.Round((fat * 9) + (carbs * 4) + (protein * 4));
            Description = description;
            MealSourceType = mealSourceType;
            Archived = false;
        }

        public static MealSource CreateCustomMealSource(float fat, float carbs, float protein) {
            return new MealSource(0, "Custom Meal", "Meal", fat, carbs, protein, "", MealSourceType.Custom);
        }

        public static bool operator ==(MealSource mealSource1, MealSource mealSource2) {
            return mealSource1.Equals(mealSource2);
        }

        public static bool operator !=(MealSource mealSource1, MealSource mealSource2) {
            return !mealSource1.Equals(mealSource2);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return string.Format("ID: {0}, Name: {1}, Serving Size: {2}, [ Fat: {3}, Carbs: {4}, Protein: {5}, Calories: {6} ], Description: {7}, Meal Type: {8}, Archived: {9}",
                Id, Name, ServingSize , Fat, Carbs, Protein, Calories, Description, MealSourceType, Archived);
        }
    }
}
