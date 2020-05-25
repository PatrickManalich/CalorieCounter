using CalorieCounter.MealSources;
using UnityEditor;
using Utils = CalorieCounter.UnitTests.EqualityTestUtilities;

namespace CalorieCounter.UnitTests
{
    public static class MealSourceEqualityTests
    {
        [MenuItem(Utils.MenuItemDirectory + "Meal Source")]
        private static void RunMealSourceEqualityTests()
        {
            Utils.Reset();
            RunBothNullTest();
            RunLeftNullTest();
            RunRightNullTest();
            RunSameReferenceTest();
            RunSameValuesTest();
            RunDifferentIdsTest();
            RunDifferentServingSizesTest();
            RunDifferentDescriptionsTest();
            RunDifferentMealSourceTypesTest();
            Utils.LogFailedTests(nameof(RunMealSourceEqualityTests));
        }

        private static void RunBothNullTest()
        {
            MealSource mealSource1 = null;
            MealSource mealSource2 = null;
            Utils.RunTestAndLog(mealSource1 == mealSource2, true, nameof(RunBothNullTest));
        }

        private static void RunLeftNullTest()
        {
            MealSource mealSource1 = null;
            var mealSource2 = Utils.CreateTestMealSource();
            Utils.RunTestAndLog(mealSource1 == mealSource2, false, nameof(RunLeftNullTest));
        }

        private static void RunRightNullTest()
        {
            var mealSource1 = Utils.CreateTestMealSource();
            MealSource mealSource2 = null;
            Utils.RunTestAndLog(mealSource1 == mealSource2, false, nameof(RunRightNullTest));
        }

        private static void RunSameReferenceTest()
        {
            var mealSource1 = Utils.CreateTestMealSource();
            var mealSource2 = mealSource1;
            Utils.RunTestAndLog(mealSource1 == mealSource2, true, nameof(RunSameReferenceTest));
        }

        private static void RunSameValuesTest()
        {
            var mealSource1 = Utils.CreateTestMealSource();
            var mealSource2 = Utils.CreateTestMealSource();
            Utils.RunTestAndLog(mealSource1 == mealSource2, true, nameof(RunSameValuesTest));
        }

        private static void RunDifferentIdsTest()
        {
            var mealSource1 = Utils.CreateTestMealSourceWithId("i1");
            var mealSource2 = Utils.CreateTestMealSourceWithId("i2");
            Utils.RunTestAndLog(mealSource1 == mealSource2, false, nameof(RunDifferentIdsTest));
        }

        private static void RunDifferentServingSizesTest()
        {
            var mealSource1 = Utils.CreateTestMealSourceWithServingSize("s1");
            var mealSource2 = Utils.CreateTestMealSourceWithServingSize("s2");
            Utils.RunTestAndLog(mealSource1 == mealSource2, false, nameof(RunDifferentServingSizesTest));
        }

        private static void RunDifferentDescriptionsTest()
        {
            var mealSource1 = Utils.CreateTestMealSourceWithDescription("d1");
            var mealSource2 = Utils.CreateTestMealSourceWithDescription("d2");
            Utils.RunTestAndLog(mealSource1 == mealSource2, false, nameof(RunDifferentDescriptionsTest));
        }

        private static void RunDifferentMealSourceTypesTest()
        {
            var mealSource1 = Utils.CreateTestMealSourceWithMealSourceType(MealSourceType.Small);
            var mealSource2 = Utils.CreateTestMealSourceWithMealSourceType(MealSourceType.Large);
            Utils.RunTestAndLog(mealSource1 == mealSource2, false, nameof(RunDifferentMealSourceTypesTest));
        }
    }
}