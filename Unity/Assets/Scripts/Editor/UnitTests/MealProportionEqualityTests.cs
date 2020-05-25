using CalorieCounter.MealEntries;
using UnityEditor;
using Utils = CalorieCounter.UnitTests.EqualityTestUtilities;

namespace CalorieCounter.UnitTests
{
    public static class MealProportionEqualityTests
    {
        [MenuItem(Utils.MenuItemDirectory + "Meal Proportion")]
        private static void RunMealProportionEqualityTests()
        {
            Utils.Reset();
            RunBothNullTest();
            RunLeftNullTest();
            RunRightNullTest();
            RunSameReferenceTest();
            RunSameValuesTest();
            RunDifferentServingAmountsTest();
            RunDifferentMealSourcesTest();
            Utils.LogFailedTests(nameof(RunMealProportionEqualityTests));
        }

        private static void RunBothNullTest()
        {
            MealProportion mealProportion1 = null;
            MealProportion mealProportion2 = null;
            Utils.RunTestAndLog(mealProportion1 == mealProportion2, true, nameof(RunBothNullTest));
        }

        private static void RunLeftNullTest()
        {
            MealProportion mealProportion1 = null;
            var mealProportion2 = Utils.CreateTestMealProportion();
            Utils.RunTestAndLog(mealProportion1 == mealProportion2, false, nameof(RunLeftNullTest));
        }

        private static void RunRightNullTest()
        {
            var mealProportion1 = Utils.CreateTestMealProportion();
            MealProportion mealProportion2 = null;
            Utils.RunTestAndLog(mealProportion1 == mealProportion2, false, nameof(RunRightNullTest));
        }

        private static void RunSameReferenceTest()
        {
            var mealProportion1 = Utils.CreateTestMealProportion();
            var mealProportion2 = mealProportion1;
            Utils.RunTestAndLog(mealProportion1 == mealProportion2, true, nameof(RunSameReferenceTest));
        }

        private static void RunSameValuesTest()
        {
            var mealProportion1 = Utils.CreateTestMealProportion();
            var mealProportion2 = Utils.CreateTestMealProportion();
            Utils.RunTestAndLog(mealProportion1 == mealProportion2, true, nameof(RunSameValuesTest));
        }

        private static void RunDifferentServingAmountsTest()
        {
            var mealProportion1 = Utils.CreateTestMealProportionWithServingAmount(1);
            var mealProportion2 = Utils.CreateTestMealProportionWithServingAmount(2);
            Utils.RunTestAndLog(mealProportion1 == mealProportion2, false, nameof(RunDifferentServingAmountsTest));
        }

        private static void RunDifferentMealSourcesTest()
        {
            var mealProportion1 = Utils.CreateTestMealProportionWithMealSource(Utils.CreateTestMealSourceWithId("i1"));
            var mealProportion2 = Utils.CreateTestMealProportionWithMealSource(Utils.CreateTestMealSourceWithId("i2"));
            Utils.RunTestAndLog(mealProportion1 == mealProportion2, false, nameof(RunDifferentMealSourcesTest));
        }
    }
}