
namespace CalorieCounter.MealSources {

    public struct MealSource {

        public string id;
        public string servingSize;
        public float fat;
        public float carbs;
        public float protein;
        public float calories;
        public string description;
        public MealSourceType mealSourceType;
        public bool archived;

        private const string CustomMealSourceServingSize = "Meal";

        public MealSource(string id, string servingSize, float fat, float carbs, float protein, string description, MealSourceType mealSourceType)
        {
            this.id = id;
            this.servingSize = servingSize;
            this.fat = fat > 0 ? GlobalMethods.Round(fat) : 0;
            this.carbs = carbs > 0 ? GlobalMethods.Round(carbs) : 0;
            this.protein = protein > 0 ? GlobalMethods.Round(protein) : 0;
            calories = GlobalMethods.Round((fat * 9) + (carbs * 4) + (protein * 4));
            this.description = description;
            this.mealSourceType = mealSourceType;
            archived = false;
        }

        public static MealSource CreateCustomMealSource(string id, float fat, float carbs, float protein) {
            return new MealSource(id, CustomMealSourceServingSize, fat, carbs, protein, "", MealSourceType.Custom);
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
            return string.Format("ID: {0}, Serving Size: {1}, [ Fat: {2}, Carbs: {3}, Protein: {4}, Calories: {5} ], Description: {6}, Meal Type: {7}, Archived: {8}",
                id, servingSize , fat, carbs, protein, calories, description, mealSourceType, archived);
        }
    }
}
