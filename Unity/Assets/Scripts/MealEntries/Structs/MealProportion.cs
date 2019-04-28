using System;

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
            Fat = Round(source.Fat * ServingAmount);
            Carbs = Round(source.Carbs * ServingAmount);
            Protein = Round(source.Protein * ServingAmount);
            Calories = Round((Fat * 9) + (Carbs * 4) + (Protein * 4));
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

        private static float Round(float number) {
            return (float)Math.Round(number, 1);
        }
    }
}
