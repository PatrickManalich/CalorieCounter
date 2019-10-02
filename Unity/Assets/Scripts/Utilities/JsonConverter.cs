using System.IO;
using Newtonsoft.Json;
using System;

namespace CalorieCounter.Utilities {

    public static class JsonConverter {

        public static void Export<T>(T value, string filePath) {
            string fullFilePath = GetFullJsonFilePath(filePath);
            using (StreamWriter file = File.CreateText(fullFilePath)) {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, value);
            }
        }

        public static T Import<T>(string filePath) {
            string fullFilePath = GetFullJsonFilePath(filePath);
            T value = Activator.CreateInstance<T>();

            if (File.Exists(fullFilePath)) {
                using (StreamReader file = File.OpenText(fullFilePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    value = (T)serializer.Deserialize(file, typeof(T));
                }
            }
            return value;
        }

        private static string GetFullJsonFilePath(string filePath) {
            string fullJsonFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.JsonDirectoryName, filePath));
            string fullJsonFilePathDir = Path.GetDirectoryName(fullJsonFilePath);

            if (!Directory.Exists(fullJsonFilePathDir)) {
                Directory.CreateDirectory(fullJsonFilePathDir);
            }
            return fullJsonFilePath;
        }

    }
}
