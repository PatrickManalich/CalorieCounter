using System;

namespace CalorieCounter {

    public enum MealSourceType { Small, Large, Custom }

    public enum DayType { None, Rest, Training, Vacation }

    public enum HighlightedType { Enter, Exit }

    public enum TextModifiedType { Instantiated, Destroying }

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

    public enum InputKeyCode { ToggleMenu, DeleteRow, RenameRow }

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

    }

    public static class GlobalMethods {

        public static float Round(float number) {
            return (float)Math.Round(number, 1);
        }

    }
}