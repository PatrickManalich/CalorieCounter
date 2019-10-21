﻿using CalorieCounter.MealSources;

namespace CalorieCounter.MealEntries {

    public struct MealProportion {

        public float servingAmount;
        public MealSource mealSource;
        public float fat;
        public float carbs;
        public float protein;
        public float calories;

        public MealProportion(float servingAmount, MealSource mealSource) {
            this.servingAmount = servingAmount;
            this.mealSource = mealSource;
            fat = GlobalMethods.Round(mealSource.fat * this.servingAmount);
            carbs = GlobalMethods.Round(mealSource.carbs * this.servingAmount);
            protein = GlobalMethods.Round(mealSource.protein * this.servingAmount);
            calories = GlobalMethods.Round((fat * 9) + (carbs * 4) + (protein * 4));
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
            return string.Format("Serving Amount: {0}, Meal Source ID: {1}, [ Fat: {2}, Carbs: {3}, Protein: {4}, Calories: {5} ]",
                servingAmount, mealSource.id, fat, carbs, protein, calories);
        }
    }
}
