using CalorieCounter.Globals;

namespace CalorieCounter.MealEntries {

    public struct MealProportion {

        public float ServingAmount;
        public MealSource MealSource;
        public float Fat;
        public float Carbs;
        public float Protein;
        public float Calories;

        public MealProportion(float servingAmount, MealSource mealSource) {
            ServingAmount = servingAmount;
            MealSource = mealSource;
            Fat = GlobalMethods.Round(mealSource.Fat * ServingAmount);
            Carbs = GlobalMethods.Round(mealSource.Carbs * ServingAmount);
            Protein = GlobalMethods.Round(mealSource.Protein * ServingAmount);
            Calories = GlobalMethods.Round((Fat * 9) + (Carbs * 4) + (Protein * 4));
        }

        public static bool operator ==(MealProportion mealProportion1, MealProportion mealProportion2) {
            return mealProportion1.Equals(mealProportion2);
        }

        public static bool operator !=(MealProportion mealProportion1, MealProportion mealProportion2) {
            return !mealProportion1.Equals(mealProportion2);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return string.Format("Serving Amount: {0}, Meal Source Name: {1}, [ Fat: {2}, Carbs: {3}, Protein: {4}, Calories: {5} ]",
                ServingAmount, MealSource.Name, Fat, Carbs, Protein, Calories);
        }
    }
}
