using System;

namespace CalorieCounter.Globals {

    public enum MealType { Small, Large, Custom }

    public enum DayType { None, Rest, Training, Vacation }

    public static class GlobalPaths {

        public const string MealEntriesDir = @"MealEntries";
        public const string MealEntryFilePrefix = @"MealEntry";
        public const string MealEntryFileExtension = @".json";
        public const string ScaleEntriesFilePath = @"ScaleEntries.json";
        public const string TargetEntriesFilePath = @"TargetEntries.json";
        public const string JsonDirPath = @"../../Json";

    }

    public static class GlobalMethods {

        public static float Round(float number) {
            return (float)Math.Round(number, 1);
        }

    }
}