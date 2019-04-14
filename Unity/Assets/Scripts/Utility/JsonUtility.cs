using System.IO;
using System.Collections.Generic;
using CalorieCounter.ScaleEntries;
using Newtonsoft.Json;

namespace CalorieCounter {

    public static class JsonUtility {

        private const string localJsonDirPath = @"../../Json";
        private const string scaleEntriesFileName = @"ScaleEntries.json";

        public static void Export(string dataPath, List<ScaleEntry> scaleEntries) {
            string scaleEntriesFilePath = Path.Combine(GetJsonDirPath(dataPath), scaleEntriesFileName);
            using (StreamWriter file = File.CreateText(scaleEntriesFilePath)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, scaleEntries);
            }
        }

        public static List<ScaleEntry> Import(string dataPath) {
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

        private static string GetJsonDirPath(string dataPath) {
            string jsonDirPath = Path.GetFullPath(Path.Combine(dataPath, localJsonDirPath));
            if (!Directory.Exists(jsonDirPath)) {
                Directory.CreateDirectory(jsonDirPath);
            }
            return jsonDirPath;
        }

    }
}
