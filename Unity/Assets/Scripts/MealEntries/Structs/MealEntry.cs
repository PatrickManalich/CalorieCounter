using System;
using System.Collections.Generic;
using System.Linq;

namespace CalorieCounter.MealEntries {

    public struct MealEntry {

        public DateTime dateTime;
        public DayType dayType;
        public float totalFat;
        public float totalCarbs;
        public float totalProtein;
        public float totalCalories;
        public Dictionary<MealSourceType, List<MealProportion>> mealProportionsDictionary;

        public MealEntry(DateTime dateTime, DayType dayType, float totalFat, float totalCarbs, float totalProtein, float totalCalories, Dictionary<MealSourceType, List<MealProportion>> mealProportionsDictionary) {
            this.dateTime = dateTime;
            this.dayType = dayType;
            this.totalFat = GlobalMethods.Round(totalFat);
            this.totalCarbs = GlobalMethods.Round(totalCarbs);
            this.totalProtein = GlobalMethods.Round(totalProtein);
            this.totalCalories = GlobalMethods.Round(totalCalories);
            this.mealProportionsDictionary = mealProportionsDictionary;
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
            if (mealProportionsDictionary != null) {
                foreach(var mealSourceType in Enum.GetValues(typeof(MealSourceType)).Cast<MealSourceType>().ToList()) {
                    mealProportionsDictCount += mealProportionsDictionary[mealSourceType].Count;
                }
            }
            return $"Date: {dateTime.ToShortDateString()}, Day Type: {dayType}, [ Total Fat: {totalFat}, " +
                $"Total Carbs: {totalCarbs}, Total Protein: {totalProtein}, Total Calories: {totalCalories} ], " +
                $"Meal Proportions Dict Count: {mealProportionsDictCount}";
        }
    }
}
