using System.IO;
using Newtonsoft.Json;
using System;
using CalorieCounter.MealEntries;
using System.Collections.Generic;

namespace CalorieCounter.Utilities {

    public static class JsonConverter {

        public static readonly string DefaultJsonDirectoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.JsonDirectoryName));
        public static readonly string ReleaseJsonDirectoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\",
            GlobalPaths.CalorieCounterReleaseDirectoryName, GlobalPaths.ReleaseDirectoryName, GlobalPaths.JsonDirectoryName));

        public static bool DoesFileExist(string fileName)
        {
            var filePath = GetJsonFilePath(fileName);
            return File.Exists(filePath);
        }

        public static void ExportFile<T>(T value, string fileName) {
            var filePath = GetJsonFilePath(fileName);
            using (StreamWriter file = File.CreateText(filePath)) {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, value);
            }
        }

        public static T ImportFile<T>(string fileName) {
            var filePath = GetJsonFilePath(fileName);
            if (File.Exists(filePath)) {
                using (StreamReader file = File.OpenText(filePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    return (T)serializer.Deserialize(file, typeof(T));
                }
            }
            return default;
        }

        public static SortedList<DateTime, MealEntry> ImportMealEntries()
        {
            var directoryPath = Path.Combine(GetJsonDirectoryPath(), GlobalPaths.MealEntriesDirectoryName);
            var mealEntries = new SortedList<DateTime, MealEntry>();
            var directoryInfo = new DirectoryInfo(directoryPath);
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                var mealEntryPath = Path.Combine(GlobalPaths.MealEntriesDirectoryName, fileInfo.Name);
                var mealEntry = ImportFile<MealEntry>(mealEntryPath);
                mealEntries.Add(mealEntry.DateTime, mealEntry);
            }
            return mealEntries;
        }

        public static string GetMealEntryPath(DateTime dateTime)
        {
            var mealEntryFileDate = "-" + dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day;
            var mealEntryFileName = GlobalPaths.MealEntryFilePrefix + mealEntryFileDate + GlobalPaths.JsonFileExtension;
            return Path.Combine(GlobalPaths.MealEntriesDirectoryName, mealEntryFileName);
        }

        private static string GetJsonFilePath(string fileName) {
            var jsonFilePath = Path.GetFullPath(Path.Combine(GetJsonDirectoryPath(), fileName));
            var jsonFilePathDirectory = Path.GetDirectoryName(jsonFilePath);

            if (!Directory.Exists(jsonFilePathDirectory)) {
                Directory.CreateDirectory(jsonFilePathDirectory);
            }
            return jsonFilePath;
        }

        private static string GetJsonDirectoryPath()
        {
#if UNITY_EDITOR
            return ReleaseJsonDirectoryPath;
#else
            return DefaultJsonDirectoryPath;
#endif
        }
    }
}
