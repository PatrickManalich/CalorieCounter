using CalorieCounter.MealSources;
using System;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    [Serializable]
    public class MealProportion {

        public float servingAmount;

        public MealSource mealSource;

        [HideInInspector]
        public float fat;

        [HideInInspector]
        public float carbs;

        [HideInInspector]
        public float protein;

        [HideInInspector]
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
            return $"Serving Amount: {servingAmount}, Meal Source ID: {mealSource.id}, [ Fat: {fat}, Carbs: {carbs}, " +
                $"Protein: {protein}, Calories: {calories} ]";
        }
    }
}
