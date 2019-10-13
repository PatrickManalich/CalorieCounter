using System.IO;
using Newtonsoft.Json;
using System;
using CalorieCounter.MealEntries;
using System.Collections.Generic;

namespace CalorieCounter.Utilities {

    public static class JsonConverter {

        public static void ExportFile<T>(T value, string fileName, bool intoRelease = false) {
            string fullFilePath = GetFullJsonFilePath(fileName, intoRelease);
            using (StreamWriter file = File.CreateText(fullFilePath)) {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, value);
            }
        }

        public static T ImportFile<T>(string fileName, bool fromRelease = false) {
            string fullFilePath = GetFullJsonFilePath(fileName, fromRelease);
            T value = Activator.CreateInstance<T>();

            if (File.Exists(fullFilePath)) {
                using (StreamReader file = File.OpenText(fullFilePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    value = (T)serializer.Deserialize(file, typeof(T));
                }
            }
            return value;
        }

        public static SortedList<DateTime, MealEntry> ImportMealEntries(bool fromRelease = false)
        {
            string fullDirectoryPath = GetFullJsonDirectoryPath(GlobalPaths.MealEntriesDirectoryName, fromRelease);
            var mealEntries = new SortedList<DateTime, MealEntry>();
            var directoryInfo = new DirectoryInfo(fullDirectoryPath);
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

        private static string GetFullJsonFilePath(string fileName, bool useReleasePath = false) {
            var fullEditorJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.JsonDirectoryName, fileName));
            var fullReleaseJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\",
                    GlobalPaths.CalorieCounterReleaseDirectoryName, GlobalPaths.ReleaseDirectoryName, GlobalPaths.JsonDirectoryName, fileName));

            string fullJsonFilePath = useReleasePath ? fullReleaseJsonFilePath : fullEditorJsonFilePath;
            string fullJsonFilePathDirectory = Path.GetDirectoryName(fullJsonFilePath);

            if (!Directory.Exists(fullJsonFilePathDirectory)) {
                Directory.CreateDirectory(fullJsonFilePathDirectory);
            }
            return fullJsonFilePath;
        }

        private static string GetFullJsonDirectoryPath(string directoryName, bool useReleasePath = false)
        {
            var fullEditorJsonDirectoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.JsonDirectoryName, directoryName));
            var fullReleaseJsonDirectoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\",
                    GlobalPaths.CalorieCounterReleaseDirectoryName, GlobalPaths.ReleaseDirectoryName, GlobalPaths.JsonDirectoryName, directoryName));

            string fullJsonDirectoryPath = useReleasePath ? fullReleaseJsonDirectoryPath : fullEditorJsonDirectoryPath;
            return fullJsonDirectoryPath;
        }
    }
}
