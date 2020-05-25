using CalorieCounter.TargetEntries;
using System;
using UnityEditor;
using Utils = CalorieCounter.UnitTests.EqualityTestUtilities;

namespace CalorieCounter.UnitTests
{
    public static class TargetEntryEqualityTests
    {
        [MenuItem(Utils.MenuItemDirectory + "Target Entry")]
        private static void RunTargetEntryEqualityTests()
        {
            Utils.Reset();
            RunBothNullTest();
            RunLeftNullTest();
            RunRightNullTest();
            RunSameReferenceTest();
            RunSameValuesTest();
            RunDifferentDateTimesTest();
            RunDifferentWeightsTest();
            Utils.LogFailedTests(nameof(RunTargetEntryEqualityTests));
        }

        private static void RunBothNullTest()
        {
            TargetEntry targetEntry1 = null;
            TargetEntry targetEntry2 = null;
            Utils.RunTestAndLog(targetEntry1 == targetEntry2, true, nameof(RunBothNullTest));
        }

        private static void RunLeftNullTest()
        {
            TargetEntry targetEntry1 = null;
            var targetEntry2 = Utils.CreateTestTargetEntry();
            Utils.RunTestAndLog(targetEntry1 == targetEntry2, false, nameof(RunLeftNullTest));
        }

        private static void RunRightNullTest()
        {
            var targetEntry1 = Utils.CreateTestTargetEntry();
            TargetEntry targetEntry2 = null;
            Utils.RunTestAndLog(targetEntry1 == targetEntry2, false, nameof(RunRightNullTest));
        }

        private static void RunSameReferenceTest()
        {
            var targetEntry1 = Utils.CreateTestTargetEntry();
            var targetEntry2 = targetEntry1;
            Utils.RunTestAndLog(targetEntry1 == targetEntry2, true, nameof(RunSameReferenceTest));
        }

        private static void RunSameValuesTest()
        {
            var targetEntry1 = Utils.CreateTestTargetEntry();
            var targetEntry2 = Utils.CreateTestTargetEntry();
            Utils.RunTestAndLog(targetEntry1 == targetEntry2, true, nameof(RunSameValuesTest));
        }

        private static void RunDifferentDateTimesTest()
        {
            var targetEntry1 = Utils.CreateTestTargetEntryWithDateTime(DateTime.Today);
            var targetEntry2 = Utils.CreateTestTargetEntryWithDateTime(DateTime.Today.AddDays(1));
            Utils.RunTestAndLog(targetEntry1 == targetEntry2, false, nameof(RunDifferentDateTimesTest));
        }

        private static void RunDifferentWeightsTest()
        {
            var scaleEntry1 = Utils.CreateTestTargetEntryWithWeight(1);
            var scaleEntry2 = Utils.CreateTestTargetEntryWithWeight(2);
            Utils.RunTestAndLog(scaleEntry1 == scaleEntry2, false, nameof(RunDifferentWeightsTest));
        }
    }
}