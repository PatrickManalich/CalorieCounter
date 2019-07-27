using System;

namespace CalorieCounter {

    public enum MealSourceType { Small, Large, Custom }

    public enum DayType { None, Rest, Training, Vacation }

    public enum HighlightedEventType { Enter, Exit }

    public enum Scene {
        Menu = 0,
        MealEntries = 1,
        MealSources = 2,
        ScaleEntries = 3,
    }

    public static class GlobalPaths {

        public const string MealEntriesDir = @"MealEntries";
        public const string MealEntryFilePrefix = @"MealEntry";
        public const string MealEntryFileExtension = @".json";
        public const string ScaleEntriesFilePath = @"ScaleEntries.json";
        public const string TargetEntriesFilePath = @"TargetEntries.json";
        public const string MealSourcesFilePath = @"MealSources.json";
        public const string JsonDirPath = @"../../Json";

    }

    public static class GlobalMethods {

        public static float Round(float number) {
            return (float)Math.Round(number, 1);
        }

    }
}