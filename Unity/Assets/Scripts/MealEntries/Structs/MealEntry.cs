using System;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public struct MealEntry {

        public DateTime Date;
        public MealSource TotalMeal;
        public Dictionary<MealTypes, List<MealSource>> MealProportionsDict;

        public MealEntry(DateTime date, MealSource totalMeal, Dictionary<MealTypes, List<MealSource>> mealProportionsDict) {
            Date = date;
            TotalMeal = totalMeal;
            MealProportionsDict = mealProportionsDict;
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
