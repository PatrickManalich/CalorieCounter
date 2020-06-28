using CalorieCounter.MealEntries;
using UnityEditor;
using Utils = CalorieCounter.UnitTests.EqualityTestUtilities;

namespace CalorieCounter.UnitTests
{
    public static class MealSuggestionEqualityTests
    {
        [MenuItem(Utils.MenuItemDirectory + "Meal Suggestion")]
        private static void RunMealSuggestionEqualityTests()
        {
            Utils.Reset();
            RunBothNullTest();
            RunLeftNullTest();
            RunRightNullTest();
            RunSameReferenceTest();
            RunSameValuesTest();
            RunDifferentMealProportionsTest();
            RunDifferentPrioritiesTest();
            Utils.LogFailedTests(nameof(RunMealSuggestionEqualityTests));
        }

        private static void RunBothNullTest()
        {
            MealSuggestion mealSuggestion1 = null;
            MealSuggestion mealSuggestion2 = null;
            Utils.RunTestAndLog(mealSuggestion1 == mealSuggestion2, true, nameof(RunBothNullTest));
        }

        private static void RunLeftNullTest()
        {
            MealSuggestion mealSuggestion1 = null;
            var mealSuggestion2 = Utils.CreateTestMealSuggestion();
            Utils.RunTestAndLog(mealSuggestion1 == mealSuggestion2, false, nameof(RunLeftNullTest));
        }

        private static void RunRightNullTest()
        {
            var mealSuggestion1 = Utils.CreateTestMealSuggestion();
            MealSuggestion mealSuggestion2 = null;
            Utils.RunTestAndLog(mealSuggestion1 == mealSuggestion2, false, nameof(RunRightNullTest));
        }

        private static void RunSameReferenceTest()
        {
            var mealSuggestion1 = Utils.CreateTestMealSuggestion();
            var mealSuggestion2 = mealSuggestion1;
            Utils.RunTestAndLog(mealSuggestion1 == mealSuggestion2, true, nameof(RunSameReferenceTest));
        }

        private static void RunSameValuesTest()
        {
            var mealSuggestion1 = Utils.CreateTestMealSuggestion();
            var mealSuggestion2 = Utils.CreateTestMealSuggestion();
            Utils.RunTestAndLog(mealSuggestion1 == mealSuggestion2, true, nameof(RunSameValuesTest));
        }

        private static void RunDifferentMealProportionsTest()
        {
            var mealSuggestion1 = Utils.CreateTestMealSuggestionWithMealProportion(Utils.CreateTestMealProportionWithServingAmount(1));
            var mealSuggestion2 = Utils.CreateTestMealSuggestionWithMealProportion(Utils.CreateTestMealProportionWithServingAmount(2));
            Utils.RunTestAndLog(mealSuggestion1 == mealSuggestion2, false, nameof(RunDifferentMealProportionsTest));
        }

        private static void RunDifferentPrioritiesTest()
        {
            var mealSuggestion1 = Utils.CreateTestMealSuggestionWithPriority(1);
            var mealSuggestion2 = Utils.CreateTestMealSuggestionWithPriority(2);
            Utils.RunTestAndLog(mealSuggestion1 == mealSuggestion2, false, nameof(RunDifferentPrioritiesTest));
        }
    }
}