using System;
using System.Collections.Generic;
using System.Linq;

namespace CalorieCounter.MealEntries {

    public struct MealEntry {

        public DateTime Date;
        public DayType DayType;
        public float TotalFat;
        public float TotalCarbs;
        public float TotalProtein;
        public float TotalCalories;
        public Dictionary<MealSourceType, List<MealProportion>> MealProportionsDictionary;

        public MealEntry(DateTime date, DayType dayType, float totalFat, float totalCarbs, float totalProtein, float totalCalories, Dictionary<MealSourceType, List<MealProportion>> mealProportionsDict) {
            Date = date;
            DayType = dayType;
            TotalFat = GlobalMethods.Round(totalFat);
            TotalCarbs = GlobalMethods.Round(totalCarbs);
            TotalProtein = GlobalMethods.Round(totalProtein);
            TotalCalories = GlobalMethods.Round(totalCalories);
            MealProportionsDictionary = mealProportionsDict;
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
            int mealProportionsDictCount = 0;
            if (MealProportionsDictionary != null) {
                foreach(var mealSourceType in Enum.GetValues(typeof(MealSourceType)).Cast<MealSourceType>().ToList()) {
                    mealProportionsDictCount += MealProportionsDictionary[mealSourceType].Count;
                }
            }
            return string.Format("Date: {0}, Day Type: {1}, [ Total Fat: {2}, Total Carbs: {3}, Total Protein: {4}, Total Calories: {5} ], Meal Proportions Dict Count: {6}",
                Date.ToShortDateString(), DayType, TotalFat, TotalCarbs, TotalProtein, TotalCalories, mealProportionsDictCount);
        }
    }
}
