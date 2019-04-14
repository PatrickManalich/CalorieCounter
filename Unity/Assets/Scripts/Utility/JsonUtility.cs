using System.IO;
using System.Collections.Generic;
using CalorieCounter.ScaleEntries;
using Newtonsoft.Json;
using CalorieCounter.TargetEntries;

namespace CalorieCounter {

    public static class JsonUtility {

        private const string localJsonDirPath = @"../../Json";
        private const string scaleEntriesFileName = @"ScaleEntries.json";
        private const string targetEntriesFileName = @"TargetEntries.json";

        public static void Export(string dataPath, List<ScaleEntry> scaleEntries) {
            string scaleEntriesFilePath = Path.Combine(GetJsonDirPath(dataPath), scaleEntriesFileName);
            using (StreamWriter file = File.CreateText(scaleEntriesFilePath)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, scaleEntries);
            }
        }

        public static void Export(string dataPath, List<TargetEntry> targetEntries) {
            string targetEntriesFilePath = Path.Combine(GetJsonDirPath(dataPath), targetEntriesFileName);
            using (StreamWriter file = File.CreateText(targetEntriesFilePath)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, targetEntries);
            }
        }

        public static List<ScaleEntry> ImportScaleEntries(string dataPath) {
            string scaleEntriesFilePath = Path.Combine(GetJsonDirPath(dataPath), scaleEntriesFileName);
            List<ScaleEntry> scaleEntries = new List<ScaleEntry>();

            if (File.Exists(scaleEntriesFilePath)) {
                using (StreamReader file = File.OpenText(scaleEntriesFilePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    scaleEntries = (List<ScaleEntry>)serializer.Deserialize(file, typeof(List<ScaleEntry>));
                }
            }
            return scaleEntries;
        }

        public static List<TargetEntry> ImportTargetEntries(string dataPath) {
            string targetEntriesFilePath = Path.Combine(GetJsonDirPath(dataPath), scaleEntriesFileName);
            List<TargetEntry> targetEntries = new List<TargetEntry>();

            if (File.Exists(targetEntriesFilePath)) {
                using (StreamReader file = File.OpenText(targetEntriesFilePath)) {
                    JsonSerializer serializer = new JsonSerializer();
                    targetEntries = (List<TargetEntry>)serializer.Deserialize(file, typeof(List<TargetEntry>));
                }
            }
            return targetEntries;
        }

        private static string GetJsonDirPath(string dataPath) {
            string jsonDirPath = Path.GetFullPath(Path.Combine(dataPath, localJsonDirPath));
            if (!Directory.Exists(jsonDirPath)) {
                Directory.CreateDirectory(jsonDirPath);
            }
            return jsonDirPath;
        }

    }
}
