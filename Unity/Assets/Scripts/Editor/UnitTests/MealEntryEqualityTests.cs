using CalorieCounter.MealEntries;
using System;
using System.Collections.Generic;
using UnityEditor;
using Utils = CalorieCounter.UnitTests.EqualityTestUtilities;

namespace CalorieCounter.UnitTests
{
    public static class MealEntryEqualityTests
    {
        [MenuItem(Utils.MenuItemDirectory + "Meal Entry")]
        private static void RunMealEntryEqualityTests()
        {
            Utils.Reset();
            RunBothNullTest();
            RunLeftNullTest();
            RunRightNullTest();
            RunSameReferenceTest();
            RunSameValuesTest();
            RunSameValuesWithKeysInDifferentOrderTest();
            RunSameValuesWithValuesInDifferentOrderTest();
            RunDifferentDateTimesTest();
            RunDifferentDayTypesTest();
            RunDifferentMealProportionsDictionariesTest();
            Utils.LogFailedTests(nameof(RunMealEntryEqualityTests));
        }

        private static void RunBothNullTest()
        {
            MealEntry mealEntry1 = null;
            MealEntry mealEntry2 = null;
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, true, nameof(RunBothNullTest));
        }

        private static void RunLeftNullTest()
        {
            MealEntry mealEntry1 = null;
            var mealEntry2 = Utils.CreateTestMealEntry();
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, false, nameof(RunLeftNullTest));
        }

        private static void RunRightNullTest()
        {
            var mealEntry1 = Utils.CreateTestMealEntry();
            MealEntry mealEntry2 = null;
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, false, nameof(RunRightNullTest));
        }

        private static void RunSameReferenceTest()
        {
            var mealEntry1 = Utils.CreateTestMealEntry();
            var mealEntry2 = mealEntry1;
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, true, nameof(RunSameReferenceTest));
        }

        private static void RunSameValuesTest()
        {
            var mealEntry1 = Utils.CreateTestMealEntry();
            var mealEntry2 = Utils.CreateTestMealEntry();
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, true, nameof(RunSameValuesTest));
        }

        private static void RunSameValuesWithKeysInDifferentOrderTest()
        {
            var mealProportionsDictionary1 = new Dictionary<MealSourceType, List<MealProportion>>()
            {
                { MealSourceType.Small, new List<MealProportion>() { Utils.CreateTestMealProportion() } },
                { MealSourceType.Large, new List<MealProportion>() { Utils.CreateTestMealProportion() } },
            };
            var mealProportionsDictionary2 = new Dictionary<MealSourceType, List<MealProportion>>()
            {
                { MealSourceType.Large, new List<MealProportion>() { Utils.CreateTestMealProportion() } },
                { MealSourceType.Small, new List<MealProportion>() { Utils.CreateTestMealProportion() } },
            };
            var mealEntry1 = Utils.CreateTestMealEntryWithMealProportionsDictionary(mealProportionsDictionary1);
            var mealEntry2 = Utils.CreateTestMealEntryWithMealProportionsDictionary(mealProportionsDictionary2);
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, true, nameof(RunSameValuesWithKeysInDifferentOrderTest));
        }

        private static void RunSameValuesWithValuesInDifferentOrderTest()
        {
            var mealProportionsDictionary1 = new Dictionary<MealSourceType, List<MealProportion>>()
            {
                { MealSourceType.Small, new List<MealProportion>() { Utils.CreateTestMealProportionWithServingAmount(1),
                    Utils.CreateTestMealProportionWithServingAmount(2) } },
            };
            var mealProportionsDictionary2 = new Dictionary<MealSourceType, List<MealProportion>>()
            {
                { MealSourceType.Small, new List<MealProportion>() { Utils.CreateTestMealProportionWithServingAmount(2),
                    Utils.CreateTestMealProportionWithServingAmount(1) } },
            };
            var mealEntry1 = Utils.CreateTestMealEntryWithMealProportionsDictionary(mealProportionsDictionary1);
            var mealEntry2 = Utils.CreateTestMealEntryWithMealProportionsDictionary(mealProportionsDictionary2);
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, true, nameof(RunSameValuesWithValuesInDifferentOrderTest));
        }

        private static void RunDifferentDateTimesTest()
        {
            var mealEntry1 = Utils.CreateTestMealEntryWithDateTime(DateTime.Today);
            var mealEntry2 = Utils.CreateTestMealEntryWithDateTime(DateTime.Today.AddDays(1));
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, false, nameof(RunDifferentDateTimesTest));
        }

        private static void RunDifferentDayTypesTest()
        {
            var mealEntry1 = Utils.CreateTestMealEntryWithDayType(DayType.Rest);
            var mealEntry2 = Utils.CreateTestMealEntryWithDayType(DayType.Training);
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, false, nameof(RunDifferentDayTypesTest));
        }

        private static void RunDifferentMealProportionsDictionariesTest()
        {
            var mealProportionsDictionary1 = new Dictionary<MealSourceType, List<MealProportion>>()
            {
                { MealSourceType.Small, new List<MealProportion>() { Utils.CreateTestMealProportionWithServingAmount(1) } },
            };
            var mealProportionsDictionary2 = new Dictionary<MealSourceType, List<MealProportion>>()
            {
                { MealSourceType.Small, new List<MealProportion>() { Utils.CreateTestMealProportionWithServingAmount(2) } },
            };
            var mealEntry1 = Utils.CreateTestMealEntryWithMealProportionsDictionary(mealProportionsDictionary1);
            var mealEntry2 = Utils.CreateTestMealEntryWithMealProportionsDictionary(mealProportionsDictionary2);
            Utils.RunTestAndLog(mealEntry1 == mealEntry2, false, nameof(RunDifferentMealProportionsDictionariesTest));
        }
    }
}