using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System;

namespace CalorieCounter.Utilities {

    public static class JsonConverter {

        public static void Export<T>(T value, string filePath) {
            string fullFilePath = GetFullFilePath(filePath);
            using (StreamWriter file = File.CreateText(fullFilePath)) {
                JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
                serializer.Serialize(file, value);
            }
        }

        public static T Import<T>(string filePath) {
            string fullFilePath = GetFullFilePath(filePath);
            T value = Activator.CreateInstance<T>();

            if (File.Exists(fullFilePath)) {
                using (StreamReader file = File.OpenText(fullFilePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    value = (T)serializer.Deserialize(file, typeof(T));
                }
            }
            return value;
        }

        private static string GetFullFilePath(string filePath) {
            string fullFilePath = Path.GetFullPath(Path.Combine(Application.dataPath, GlobalPaths.JsonDirPath, filePath));
            string fullFilePathDir = Path.GetDirectoryName(fullFilePath);

            if (!Directory.Exists(fullFilePathDir)) {
                Directory.CreateDirectory(fullFilePathDir);
            }
            return fullFilePath;
        }

    }
}
