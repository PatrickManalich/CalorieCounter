using System.IO;
using Newtonsoft.Json;
using System;

namespace CalorieCounter.Utilities {

    public static class JsonConverter {

        public static void Export<T>(T value, string filePath, bool releasePath = false) {
            string fullFilePath = GetFullJsonFilePath(filePath, releasePath);
            using (StreamWriter file = File.CreateText(fullFilePath)) {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, value);
            }
        }

        public static T Import<T>(string filePath, bool releasePath = false) {
            string fullFilePath = GetFullJsonFilePath(filePath, releasePath);
            T value = Activator.CreateInstance<T>();

            if (File.Exists(fullFilePath)) {
                using (StreamReader file = File.OpenText(fullFilePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    value = (T)serializer.Deserialize(file, typeof(T));
                }
            }
            return value;
        }

        public static string GetMealEntryPath(DateTime dateTime)
        {
            string mealEntryFileDate = "-" + dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day;
            string mealEntryFileName = GlobalPaths.MealEntryFilePrefix + mealEntryFileDate + GlobalPaths.JsonFileExtension;
            return Path.Combine(GlobalPaths.MealEntriesDirectoryName, mealEntryFileName);
        }

        private static string GetFullJsonFilePath(string filePath, bool releasePath = false) {
            var fullEditorJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.JsonDirectoryName, filePath));
            var fullReleaseJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\",
                    GlobalPaths.CalorieCounterReleaseDirectoryName, GlobalPaths.ReleaseDirectoryName, GlobalPaths.JsonDirectoryName, filePath));

            string fullJsonFilePath = releasePath ? fullReleaseJsonFilePath : fullEditorJsonFilePath;
            string fullJsonFilePathDir = Path.GetDirectoryName(fullJsonFilePath);

            if (!Directory.Exists(fullJsonFilePathDir)) {
                Directory.CreateDirectory(fullJsonFilePathDir);
            }
            return fullJsonFilePath;
        }

    }
}
