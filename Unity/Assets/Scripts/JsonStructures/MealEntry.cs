using Newtonsoft.Json;
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

        [JsonConstructor]
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

        public static bool operator ==(MealEntry mealEntry1, MealEntry mealEntry2)
        {
            if (mealEntry1 is null)
            {
                if (mealEntry2 is null)
                {
                    return true;
                }
                return false;
            }
            return mealEntry1.Equals(mealEntry2);
        }

        public static bool operator !=(MealEntry mealEntry1, MealEntry mealEntry2)
        {
            return !(mealEntry1 == mealEntry2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MealEntry);
        }

        public override int GetHashCode()
        {
            return (dateTime, dayType, totalFat, totalCarbs, totalProtein, totalCalories, mealProportionsDictionary).GetHashCode();
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

        public bool Equals(MealEntry other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (GetType() != other.GetType())
            {
                return false;
            }

            var mealProportionsDictionariesAreEqual = true;

            if (mealProportionsDictionary.Keys.Count == other.mealProportionsDictionary.Keys.Count && 
                mealProportionsDictionary.Keys.All(other.mealProportionsDictionary.Keys.Contains))
            {
                // Keys are equal
                foreach (var mealSourceType in mealProportionsDictionary.Keys)
                {
                    var mealProportions = mealProportionsDictionary[mealSourceType];
                    var otherMealProportions = other.mealProportionsDictionary[mealSourceType];

                    if (!mealProportions.All(otherMealProportions.Contains))
                    {
                        // Meal proportions aren't equal
                        mealProportionsDictionariesAreEqual = false;
                        break;
                    }
                }
            }
            else
            {
                mealProportionsDictionariesAreEqual = false;
            }

            return (dateTime == other.dateTime) && (dayType == other.dayType) && (totalFat == other.totalFat) &&
                (totalCarbs == other.totalCarbs) && (totalProtein == other.totalProtein) && (totalCalories == other.totalCalories) &&
                mealProportionsDictionariesAreEqual;
        }
    }
}
