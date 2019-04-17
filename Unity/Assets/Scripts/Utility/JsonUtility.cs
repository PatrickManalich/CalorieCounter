using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CalorieCounter {

    public static class JsonUtility {

        private const string JsonDirPath = @"../../Json";

        public static void ExportEntry<T>(T entry, string entryFilePath) {
            string fullFilePath = GetFullFilePath(entryFilePath);
            using (StreamWriter file = File.CreateText(fullFilePath)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entry);
            }
        }

        public static void ExportEntries<T>(List<T> entries, string entriesFilePath) {
            string fullFilePath = GetFullFilePath(entriesFilePath);
            using (StreamWriter file = File.CreateText(fullFilePath)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entries);
            }
        }

        public static List<T> ImportEntries<T>(string entryFilePath) {
            string fullFilePath = GetFullFilePath(entryFilePath);
            List<T> entries = new List<T>();

            if (File.Exists(fullFilePath)) {
                using (StreamReader file = File.OpenText(fullFilePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    entries = (List<T>)serializer.Deserialize(file, typeof(List<T>));
                }
            }
            return entries;
        }

        private static string GetFullFilePath(string filePath) {
            string fullFilePath = Path.GetFullPath(Path.Combine(Application.dataPath, JsonDirPath, filePath));
            string fullFilePathDir = Path.GetDirectoryName(fullFilePath);

            if (!Directory.Exists(fullFilePathDir)) {
                Directory.CreateDirectory(fullFilePathDir);
            }
            return fullFilePath;
        }

    }
}
