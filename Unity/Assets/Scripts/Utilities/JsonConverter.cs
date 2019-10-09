using System.IO;
using Newtonsoft.Json;
using System;

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
            string fullJsonFilePathDir = Path.GetDirectoryName(fullJsonFilePath);

            if (!Directory.Exists(fullJsonFilePathDir)) {
                Directory.CreateDirectory(fullJsonFilePathDir);
            }
            return fullJsonFilePath;
        }

    }
}
