using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalorieCounter.MealEntries {

    public class MealEntry {

        public DateTime DateTime { get; }
        public DayType DayType { get; }
        public float TotalFat { get; }
        public float TotalCarbs { get; }
        public float TotalProtein { get; }
        public float TotalCalories { get; }
        public Dictionary<MealSourceType, List<MealProportion>> MealProportionsDictionary { get; }

        public MealEntry() { }

        [JsonConstructor]
        public MealEntry(DateTime dateTime, DayType dayType, Dictionary<MealSourceType, List<MealProportion>> mealProportionsDictionary) {
            DateTime = dateTime;
            DayType = dayType;

            TotalFat = 0;
            TotalCarbs = 0;
            TotalProtein = 0;
            foreach(var mealProportions in mealProportionsDictionary.Values)
            {
                foreach(var mealProportion in mealProportions)
                {
                    TotalFat += mealProportion.Fat;
                    TotalCarbs += mealProportion.Carbs;
                    TotalProtein += mealProportion.Protein;
                }
            }
            TotalFat = GlobalMethods.Round(TotalFat);
            TotalCarbs = GlobalMethods.Round(TotalCarbs);
            TotalProtein = GlobalMethods.Round(TotalProtein);
            TotalCalories = GlobalMethods.Round((TotalFat * 9) + (TotalCarbs * 4) + (TotalProtein * 4));

            MealProportionsDictionary = mealProportionsDictionary;
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
            return (DateTime, DayType, TotalFat, TotalCarbs, TotalProtein, TotalCalories, MealProportionsDictionary).GetHashCode();
        }

        public override string ToString() {
            int mealProportionsDictCount = 0;
            if (MealProportionsDictionary != null) {
                foreach(var mealSourceType in MealProportionsDictionary.Keys) {
                    mealProportionsDictCount += MealProportionsDictionary[mealSourceType].Count;
                }
            }
            return $"Date: {DateTime.ToShortDateString()}, Day Type: {DayType}, [ Total Fat: {TotalFat}, " +
                $"Total Carbs: {TotalCarbs}, Total Protein: {TotalProtein}, Total Calories: {TotalCalories} ], " +
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

            if (MealProportionsDictionary.Keys.Count == other.MealProportionsDictionary.Keys.Count && 
                MealProportionsDictionary.Keys.All(other.MealProportionsDictionary.Keys.Contains))
            {
                // Keys are equal
                foreach (var mealSourceType in MealProportionsDictionary.Keys)
                {
                    var mealProportions = MealProportionsDictionary[mealSourceType];
                    var otherMealProportions = other.MealProportionsDictionary[mealSourceType];

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

            return (DateTime == other.DateTime) && (DayType == other.DayType) && (TotalFat == other.TotalFat) &&
                (TotalCarbs == other.TotalCarbs) && (TotalProtein == other.TotalProtein) && (TotalCalories == other.TotalCalories) &&
                mealProportionsDictionariesAreEqual;
        }
    }
}
