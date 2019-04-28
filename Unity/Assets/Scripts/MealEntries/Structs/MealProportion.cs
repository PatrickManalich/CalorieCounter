using CalorieCounter.Globals;

namespace CalorieCounter.MealEntries {

    public struct MealProportion {

        public float ServingAmount;
        public MealSource Source;
        public float Fat;
        public float Carbs;
        public float Protein;
        public float Calories;

        public MealProportion(float servingAmount, MealSource source) {
            ServingAmount = servingAmount;
            Source = source;
            Fat = GlobalMethods.Round(source.Fat * ServingAmount);
            Carbs = GlobalMethods.Round(source.Carbs * ServingAmount);
            Protein = GlobalMethods.Round(source.Protein * ServingAmount);
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
            return ServingAmount + " " + Source.Name + ", [ Fat:" + Fat + ", Carbs:" + Carbs + ", Protein:" + Protein + ", Calories:" + Calories + " ]";
        }
    }
}
