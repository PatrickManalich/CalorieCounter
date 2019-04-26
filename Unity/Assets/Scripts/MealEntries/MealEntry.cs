using System;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public struct MealEntry {

        public DateTime Date;
        public Meal TotalMeal;
        public List<Meal> MealProportions;

        public MealEntry(DateTime date, Meal totalMeal, List<Meal> mealProportions) {
            Date = date;
            TotalMeal = totalMeal;
            MealProportions = mealProportions;
        }

        public static bool operator ==(MealEntry mealEntry1, MealEntry mealEntry2) {
            return mealEntry1.Equals(mealEntry2);
        }

        public static bool operator !=(MealEntry mealEntry1, MealEntry mealEntry2) {
            return !mealEntry1.Equals(mealEntry2);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return Date.ToShortDateString() + ", TotalMeal:" + TotalMeal;
        }
    }
}
