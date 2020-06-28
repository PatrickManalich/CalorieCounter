using System;
using System.IO;

namespace CalorieCounter {

    public enum MealSourceType { Small, Large, Custom }

    public enum DayType { None, Rest, Training, Vacation }

    public enum HighlightedType { Entered, Exited }

    public enum TextModifiedType { Instantiated, Destroying }

    public enum MealProportionModifiedType { Added, Removed }

    public enum Scene {
        Menu = 0,
        MealEntries = 1,
        MealSources = 2,
        ScaleEntries = 3,
    }

    [Flags]
    public enum DaysOfTheWeek
    {
        // Don't use 0 because EnumFlagDrawer defines that value as 'Nothing/Everything'
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64
    }

    public enum InputKeyCode { ToggleMenu, RemoveRow, RenameRow, AcceptSuggestion }

    public static class GlobalConsts
    {

        public static readonly char[] ValidSpecialChars = new char[]
        {
                '-', '\'', '&', '.', ' ', '/', '%',
        };

    }

    public static class GlobalPaths {

        public const string MealEntryFilePrefix = @"MealEntry";
        public const string ScaleEntriesFilePrefix = @"ScaleEntries";
        public const string TargetEntriesFilePrefix = @"TargetEntries";
        public const string MealSourcesFilePrefix = @"MealSources";
        public const string MealSourceNamesFilePrefix = @"MealSourceNames";
        public const string ResultsFilePrefix = @"Results";

        public const string JsonFileExtension = @".json";
        public const string CsvFileExtension = @".csv";

        public const string JsonScaleEntriesFileName = ScaleEntriesFilePrefix + JsonFileExtension;
        public const string JsonTargetEntriesFileName = TargetEntriesFilePrefix + JsonFileExtension;
        public const string JsonMealSourcesFileName = MealSourcesFilePrefix + JsonFileExtension;
        public const string JsonMealSourceNamesFileName = MealSourceNamesFilePrefix + JsonFileExtension;
        public const string CsvScaleEntriesFileName = ScaleEntriesFilePrefix + CsvFileExtension;
        public const string CsvTargetEntriesFileName = TargetEntriesFilePrefix + CsvFileExtension;
        public const string CsvResultsFileName = ResultsFilePrefix + CsvFileExtension;

        public const string MealEntriesDirectoryName = @"MealEntries";
        public const string JsonDirectoryName = @"Json";
        public const string CsvDirectoryName = @"Csv";
        public const string ReleaseDirectoryName = @"Release";
        public const string CalorieCounterReleaseDirectoryName = @"CalorieCounterRelease";

        public const string ScriptableObjectsDirectoryName = @"ScriptableObjects";
        public const string DayMealPatternsDirectoryName = @"DayMealPatterns";
        public const string DayTypeMealPatternsDirectoryName = @"DayTypeMealPatterns";
        public const string GroupMealPatternsDirectoryName = @"GroupMealPatterns";

        public static readonly string DayMealPatternsPath = Path.Combine(ScriptableObjectsDirectoryName, DayMealPatternsDirectoryName);
        public static readonly string DayTypeMealPatternsPath = Path.Combine(ScriptableObjectsDirectoryName, DayTypeMealPatternsDirectoryName);
        public static readonly string GroupMealPatternsPath = Path.Combine(ScriptableObjectsDirectoryName, GroupMealPatternsDirectoryName);

    }

    public static class GlobalMethods {

        public static float Round(float number) {
            return (float)Math.Round(number, 1);
        }

    }
}