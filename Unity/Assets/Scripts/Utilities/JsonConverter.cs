using System.IO;
using Newtonsoft.Json;
using System;
using CalorieCounter.MealEntries;
using System.Collections.Generic;

namespace CalorieCounter.Utilities {

    public static class JsonConverter {

        public static void ExportFile<T>(T value, string fileName, bool intoRelease = false) {
            string filePath = GetJsonFilePath(fileName, intoRelease);
            using (StreamWriter file = File.CreateText(filePath)) {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, value);
            }
        }

        public static T ImportFile<T>(string fileName, bool fromRelease = false) {
            string filePath = GetJsonFilePath(fileName, fromRelease);
            if (File.Exists(filePath)) {
                using (StreamReader file = File.OpenText(filePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    return (T)serializer.Deserialize(file, typeof(T));
                }
            }
            return default;
        }

        public static SortedList<DateTime, MealEntry> ImportMealEntries(bool fromRelease = false)
        {
            string directoryPath = GetJsonDirectoryPath(GlobalPaths.MealEntriesDirectoryName, fromRelease);
            var mealEntries = new SortedList<DateTime, MealEntry>();
            var directoryInfo = new DirectoryInfo(directoryPath);
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                var mealEntryPath = Path.Combine(GlobalPaths.MealEntriesDirectoryName, fileInfo.Name);
                var mealEntry = ImportFile<MealEntry>(mealEntryPath, true);
                mealEntries.Add(mealEntry.dateTime, mealEntry);
            }
            return mealEntries;
        }

        public static string GetMealEntryPath(DateTime dateTime)
        {
            string mealEntryFileDate = "-" + dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day;
            string mealEntryFileName = GlobalPaths.MealEntryFilePrefix + mealEntryFileDate + GlobalPaths.JsonFileExtension;
            return Path.Combine(GlobalPaths.MealEntriesDirectoryName, mealEntryFileName);
        }

        private static string GetJsonFilePath(string fileName, bool useReleasePath = false) {
            var editorJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.JsonDirectoryName, fileName));
            var releaseJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\",
                    GlobalPaths.CalorieCounterReleaseDirectoryName, GlobalPaths.ReleaseDirectoryName, GlobalPaths.JsonDirectoryName, fileName));

            string jsonFilePath = useReleasePath ? releaseJsonFilePath : editorJsonFilePath;
            string jsonFilePathDirectory = Path.GetDirectoryName(jsonFilePath);

            if (!Directory.Exists(jsonFilePathDirectory)) {
                Directory.CreateDirectory(jsonFilePathDirectory);
            }
            return jsonFilePath;
        }

        private static string GetJsonDirectoryPath(string directoryName, bool useReleasePath = false)
        {
            var editorJsonDirectoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.JsonDirectoryName, directoryName));
            var releaseJsonDirectoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\",
                    GlobalPaths.CalorieCounterReleaseDirectoryName, GlobalPaths.ReleaseDirectoryName, GlobalPaths.JsonDirectoryName, directoryName));

            string jsonDirectoryPath = useReleasePath ? releaseJsonDirectoryPath : editorJsonDirectoryPath;
            return jsonDirectoryPath;
        }
    }
}
