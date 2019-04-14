using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CalorieCounter {

    public static class JsonUtility {

        private const string localJsonDirPath = @"../../Json";

        public static void ExportEntry<T>(T entry, string entryFileName) {
            string entryFilePath = Path.Combine(GetJsonDirPath(), entryFileName);
            using (StreamWriter file = File.CreateText(entryFilePath)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entry);
            }
        }

        public static void ExportEntries<T>(List<T> entries, string entriesFileName) {
            string entriesFilePath = Path.Combine(GetJsonDirPath(), entriesFileName);
            using (StreamWriter file = File.CreateText(entriesFilePath)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, entries);
            }
        }

        public static List<T> ImportEntries<T>(string entriesFileName) {
            string entriesFilePath = Path.Combine(GetJsonDirPath(), entriesFileName);
            List<T> entries = new List<T>();

            if (File.Exists(entriesFilePath)) {
                using (StreamReader file = File.OpenText(entriesFilePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    entries = (List<T>)serializer.Deserialize(file, typeof(List<T>));
                }
            }
            return entries;
        }

        private static string GetJsonDirPath() {
            string jsonDirPath = Path.GetFullPath(Path.Combine(Application.dataPath, localJsonDirPath));
            if (!Directory.Exists(jsonDirPath)) {
                Directory.CreateDirectory(jsonDirPath);
            }
            return jsonDirPath;
        }

    }
}
