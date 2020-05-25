using CalorieCounter.ScaleEntries;
using System;
using UnityEditor;
using Utils = CalorieCounter.UnitTests.EqualityTestUtilities;

namespace CalorieCounter.UnitTests
{
    public static class ScaleEntryEqualityTests
    {
        [MenuItem(Utils.MenuItemDirectory + "Scale Entry")]
        private static void RunScaleEntryEqualityTests()
        {
            Utils.Reset();
            RunBothNullTest();
            RunLeftNullTest();
            RunRightNullTest();
            RunSameReferenceTest();
            RunSameValuesTest();
            RunDifferentDateTimesTest();
            RunDifferentWeightsTest();
            Utils.LogFailedTests(nameof(RunScaleEntryEqualityTests));
        }

        private static void RunBothNullTest()
        {
            ScaleEntry scaleEntry1 = null;
            ScaleEntry scaleEntry2 = null;
            Utils.RunTestAndLog(scaleEntry1 == scaleEntry2, true, nameof(RunBothNullTest));
        }

        private static void RunLeftNullTest()
        {
            ScaleEntry scaleEntry1 = null;
            var scaleEntry2 = Utils.CreateTestScaleEntry();
            Utils.RunTestAndLog(scaleEntry1 == scaleEntry2, false, nameof(RunLeftNullTest));
        }

        private static void RunRightNullTest()
        {
            var scaleEntry1 = Utils.CreateTestScaleEntry();
            ScaleEntry scaleEntry2 = null;
            Utils.RunTestAndLog(scaleEntry1 == scaleEntry2, false, nameof(RunRightNullTest));
        }

        private static void RunSameReferenceTest()
        {
            var scaleEntry1 = Utils.CreateTestScaleEntry();
            var scaleEntry2 = scaleEntry1;
            Utils.RunTestAndLog(scaleEntry1 == scaleEntry2, true, nameof(RunSameReferenceTest));
        }

        private static void RunSameValuesTest()
        {
            var scaleEntry1 = Utils.CreateTestScaleEntry();
            var scaleEntry2 = Utils.CreateTestScaleEntry();
            Utils.RunTestAndLog(scaleEntry1 == scaleEntry2, true, nameof(RunSameValuesTest));
        }

        private static void RunDifferentDateTimesTest()
        {
            var scaleEntry1 = Utils.CreateTestScaleEntryWithDateTime(DateTime.Today);
            var scaleEntry2 = Utils.CreateTestScaleEntryWithDateTime(DateTime.Today.AddDays(1));
            Utils.RunTestAndLog(scaleEntry1 == scaleEntry2, false, nameof(RunDifferentDateTimesTest));
        }

        private static void RunDifferentWeightsTest()
        {
            var scaleEntry1 = Utils.CreateTestScaleEntryWithWeight(1);
            var scaleEntry2 = Utils.CreateTestScaleEntryWithWeight(2);
            Utils.RunTestAndLog(scaleEntry1 == scaleEntry2, false, nameof(RunDifferentWeightsTest));
        }
    }
}