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

        public override string ToString() {
            return Date.ToShortDateString() + ", TotalMeal:" + TotalMeal;
        }
    }
}
