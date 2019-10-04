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

        public const string JsonFileExtension = @".json";
        public const string CsvFileExtension = @".csv";

        public const string JsonScaleEntriesFileName = ScaleEntriesFilePrefix + JsonFileExtension;
        public const string JsonTargetEntriesFileName = TargetEntriesFilePrefix + JsonFileExtension;
        public const string JsonMealSourcesFileName = MealSourcesFilePrefix + JsonFileExtension;
        public const string JsonMealSourceNamesFileName = MealSourceNamesFilePrefix + JsonFileExtension;
        public const string CsvScaleEntriesFileName = ScaleEntriesFilePrefix + CsvFileExtension;
        public const string CsvTargetEntriesFileName = TargetEntriesFilePrefix + CsvFileExtension;
        public const string CsvMealSourcesFileName = MealSourcesFilePrefix + CsvFileExtension;
        public const string CsvMealSourceNamesFileName = MealSourceNamesFilePrefix + CsvFileExtension;

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