using CalorieCounter.MealSources;
using UnityEditor;
using Utils = CalorieCounter.UnitTests.EqualityTestUtilities;

namespace CalorieCounter.UnitTests
{
    public static class NamedMealSourceEqualityTests
    {
        [MenuItem(Utils.MenuItemDirectory + "Named Meal Source")]
        private static void RunNamedMealSourceEqualityTests()
        {
            Utils.Reset();
            RunBothNullTest();
            RunLeftNullTest();
            RunRightNullTest();
            RunSameReferenceTest();
            RunSameValuesTest();
            RunDifferentNamesTest();
            RunDifferentMealSourcesTest();
            Utils.LogFailedTests(nameof(RunNamedMealSourceEqualityTests));
        }

        private static void RunBothNullTest()
        {
            NamedMealSource namedMealSource1 = null;
            NamedMealSource namedMealSource2 = null;
            Utils.RunTestAndLog(namedMealSource1 == namedMealSource2, true, nameof(RunBothNullTest));
        }

        private static void RunLeftNullTest()
        {
            NamedMealSource namedMealSource1 = null;
            var namedMealSource2 = Utils.CreateTestNamedMealSource();
            Utils.RunTestAndLog(namedMealSource1 == namedMealSource2, false, nameof(RunLeftNullTest));
        }

        private static void RunRightNullTest()
        {
            var namedMealSource1 = Utils.CreateTestNamedMealSource();
            NamedMealSource namedMealSource2 = null;
            Utils.RunTestAndLog(namedMealSource1 == namedMealSource2, false, nameof(RunRightNullTest));
        }

        private static void RunSameReferenceTest()
        {
            var namedMealSource1 = Utils.CreateTestNamedMealSource();
            var namedMealSource2 = namedMealSource1;
            Utils.RunTestAndLog(namedMealSource1 == namedMealSource2, true, nameof(RunSameReferenceTest));
        }

        private static void RunSameValuesTest()
        {
            var namedMealSource1 = Utils.CreateTestNamedMealSource();
            var namedMealSource2 = Utils.CreateTestNamedMealSource();
            Utils.RunTestAndLog(namedMealSource1 == namedMealSource2, true, nameof(RunSameValuesTest));
        }

        private static void RunDifferentNamesTest()
        {
            var namedMealSource1 = Utils.CreateTestNamedMealSourceWithName("n1");
            var namedMealSource2 = Utils.CreateTestNamedMealSourceWithName("n2");
            Utils.RunTestAndLog(namedMealSource1 == namedMealSource2, false, nameof(RunDifferentNamesTest));
        }

        private static void RunDifferentMealSourcesTest()
        {
            var namedMealSource1 = Utils.CreateTestNamedMealSourceWithMealSource(Utils.CreateTestMealSourceWithId("i1"));
            var namedMealSource2 = Utils.CreateTestNamedMealSourceWithMealSource(Utils.CreateTestMealSourceWithId("i2"));
            Utils.RunTestAndLog(namedMealSource1 == namedMealSource2, false, nameof(RunDifferentMealSourcesTest));
        }
    }
}