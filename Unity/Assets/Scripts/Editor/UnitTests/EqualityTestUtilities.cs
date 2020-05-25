using CalorieCounter.MealEntries;
using CalorieCounter.MealSources;
using CalorieCounter.ScaleEntries;
using CalorieCounter.TargetEntries;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CalorieCounter.UnitTests
{
    public static class EqualityTestUtilities
	{
        public static int TestsFailed { get; private set; }

        public const string MenuItemDirectory = @"Calorie Counter/Unit Tests/";

        public static void Reset()
        {
            TestsFailed = 0;
        }

        public static void RunTestAndLog(bool value, bool expectedValue, string testMethodName)
        {
            var result = value == expectedValue;
            var resultSymbol = result ? 'O' : 'X';
            Debug.Log($"{resultSymbol} - {GetTestDisplayName(testMethodName)}");
            
            if (!result)
            {
                TestsFailed++;
            }
        }

        public static void LogFailedTests(string testMethodName)
        {
            Debug.Log("--------------------------------------------");
            Debug.Log($"Tests failed in {GetTestDisplayName(testMethodName)}: {TestsFailed}");
        }

        #region Creation Methods
        public static MealSource CreateTestMealSource()
        {
            return new MealSource("i", "s", 1, 1, 1, "d", MealSourceType.Small, false);
        }

        public static MealSource CreateTestMealSourceWithId(string id)
        {
            return new MealSource(id, "s", 1, 1, 1, "d", MealSourceType.Small, false);
        }

        public static MealSource CreateTestMealSourceWithServingSize(string servingSize)
        {
            return new MealSource("i", servingSize, 1, 1, 1, "d", MealSourceType.Small, false);
        }

        public static MealSource CreateTestMealSourceWithDescription(string description)
        {
            return new MealSource("i", "s", 1, 1, 1, description, MealSourceType.Small, false);
        }

        public static MealSource CreateTestMealSourceWithMealSourceType(MealSourceType mealSourceType)
        {
            return new MealSource("i", "s", 1, 1, 1, "d", mealSourceType, false);
        }

        public static MealProportion CreateTestMealProportion()
        {
            return new MealProportion(1, CreateTestMealSource());
        }

        public static MealProportion CreateTestMealProportionWithServingAmount(float servingAmount)
        {
            return new MealProportion(servingAmount, CreateTestMealSource());
        }

        public static MealProportion CreateTestMealProportionWithMealSource(MealSource mealSource)
        {
            return new MealProportion(1, mealSource);
        }

        public static NamedMealSource CreateTestNamedMealSource()
        {
            return new NamedMealSource("n", CreateTestMealSource());
        }

        public static NamedMealSource CreateTestNamedMealSourceWithName(string name)
        {
            return new NamedMealSource(name, CreateTestMealSource());
        }

        public static NamedMealSource CreateTestNamedMealSourceWithMealSource(MealSource mealSource)
        {
            return new NamedMealSource("n", mealSource);
        }

        public static MealSuggestion CreateTestMealSuggestion()
        {
            return new MealSuggestion(CreateTestMealProportion(), MealPatternType.Day, 1);
        }

        public static MealSuggestion CreateTestMealSuggestionWithMealProportion(MealProportion mealProportion)
        {
            return new MealSuggestion(mealProportion, MealPatternType.Day, 1);
        }

        public static MealSuggestion CreateTestMealSuggestionWithMealPatternType(MealPatternType mealPatternType)
        {
            return new MealSuggestion(CreateTestMealProportion(), mealPatternType, 1);
        }

        public static MealSuggestion CreateTestMealSuggestionWithPriority(int priority)
        {
            return new MealSuggestion(CreateTestMealProportion(), MealPatternType.Day, priority);
        }

        public static Dictionary<MealSourceType, List<MealProportion>> CreateTestMealProportionsDictionary()
        {
            return new Dictionary<MealSourceType, List<MealProportion>>()
            {
                { MealSourceType.Small, new List<MealProportion>() { CreateTestMealProportion() } },
                { MealSourceType.Large, new List<MealProportion>() { CreateTestMealProportion() } },
                { MealSourceType.Custom, new List<MealProportion>() { CreateTestMealProportion() } },
            };
        }

        public static MealEntry CreateTestMealEntry()
        {
            return new MealEntry(DateTime.Today, DayType.Rest, CreateTestMealProportionsDictionary());
        }

        public static MealEntry CreateTestMealEntryWithDateTime(DateTime dateTime)
        {
            return new MealEntry(dateTime, DayType.Rest, CreateTestMealProportionsDictionary());
        }

        public static MealEntry CreateTestMealEntryWithDayType(DayType dayType)
        {
            return new MealEntry(DateTime.Today, dayType, CreateTestMealProportionsDictionary());
        }

        public static MealEntry CreateTestMealEntryWithMealProportionsDictionary(Dictionary<MealSourceType, List<MealProportion>> mealProportionsDictionary)
        {
            return new MealEntry(DateTime.Today, DayType.Rest, mealProportionsDictionary);
        }

        public static ScaleEntry CreateTestScaleEntry()
        {
            return new ScaleEntry(DateTime.Today, 1, 1, 1, 1, 1, 1);
        }

        public static ScaleEntry CreateTestScaleEntryWithDateTime(DateTime dateTime)
        {
            return new ScaleEntry(dateTime, 1, 1, 1, 1, 1, 1);
        }

        public static ScaleEntry CreateTestScaleEntryWithWeight(float weight)
        {
            return new ScaleEntry(DateTime.Today, weight, 1, 1, 1, 1, 1);
        }

        public static TargetEntry CreateTestTargetEntry()
        {
            return new TargetEntry(DateTime.Today, 1);
        }

        public static TargetEntry CreateTestTargetEntryWithDateTime(DateTime dateTime)
        {
            return new TargetEntry(dateTime, 1);
        }

        public static TargetEntry CreateTestTargetEntryWithWeight(float weight)
        {
            return new TargetEntry(DateTime.Today, weight);
        }
        #endregion

        private static string GetTestDisplayName(string testMethodName)
        {
            var shortenedTestName = testMethodName.Replace("Run", "");
            var stringBuilder = new StringBuilder();
            var previousChar = char.MinValue;
            foreach (var c in shortenedTestName)
            {
                if (char.IsUpper(c))
                {
                    if (stringBuilder.Length != 0 && previousChar != ' ')
                    {
                        stringBuilder.Append(' ');
                    }
                }
                stringBuilder.Append(c);
                previousChar = c;
            }
            return stringBuilder.ToString();
        }
    }
}

