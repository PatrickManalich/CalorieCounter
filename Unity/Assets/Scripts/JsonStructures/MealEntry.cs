using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    [Serializable]
    public class MealEntry {

        public DateTime dateTime;

        public DayType dayType;

        [HideInInspector]
        public float totalFat;

        [HideInInspector]
        public float totalCarbs;

        [HideInInspector]
        public float totalProtein;

        [HideInInspector]
        public float totalCalories;

        public Dictionary<MealSourceType, List<MealProportion>> mealProportionsDictionary;

        public MealEntry(DateTime dateTime, DayType dayType, Dictionary<MealSourceType, List<MealProportion>> mealProportionsDictionary) {
            this.dateTime = dateTime;
            this.dayType = dayType;

            totalFat = 0;
            totalCarbs = 0;
            totalProtein = 0;
            foreach(var mealProportions in mealProportionsDictionary.Values)
            {
                foreach(var mealProportion in mealProportions)
                {
                    totalFat += mealProportion.fat;
                    totalCarbs += mealProportion.carbs;
                    totalProtein += mealProportion.protein;
                }
            }
            totalFat = GlobalMethods.Round(totalFat);
            totalCarbs = GlobalMethods.Round(totalCarbs);
            totalProtein = GlobalMethods.Round(totalProtein);
            totalCalories = GlobalMethods.Round((totalFat * 9) + (totalCarbs * 4) + (totalProtein * 4));

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
                foreach(var mealSourceType in mealProportionsDictionary.Keys) {
                    mealProportionsDictCount += mealProportionsDictionary[mealSourceType].Count;
                }
            }
            return $"Date: {dateTime.ToShortDateString()}, Day Type: {dayType}, [ Total Fat: {totalFat}, " +
                $"Total Carbs: {totalCarbs}, Total Protein: {totalProtein}, Total Calories: {totalCalories} ], " +
                $"Meal Proportions Dict Count: {mealProportionsDictCount}";
        }
    }
}
