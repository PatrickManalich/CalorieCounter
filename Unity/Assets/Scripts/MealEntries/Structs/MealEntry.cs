﻿using System;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public struct MealEntry {

        public DateTime Date;
        public float TotalFat;
        public float TotalCarbs;
        public float TotalProtein;
        public float TotalCalories;
        public Dictionary<MealTypes, List<MealProportion>> MealProportionsDict;

        public MealEntry(DateTime date, float totalFat, float totalCarbs, float totalProtein, float totalCalories, Dictionary<MealTypes, List<MealProportion>> mealProportionsDict) {
            Date = date;
            TotalFat = totalFat;
            TotalCarbs = totalCarbs;
            TotalProtein = totalProtein;
            TotalCalories = totalCalories;
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
            return Date.ToShortDateString() + ", Total Fat:" + TotalFat + ", Total Carbs:" + TotalCarbs + ", Total Protein:" + TotalProtein + ", Total Calories:" + TotalCalories;
        }
    }
}
