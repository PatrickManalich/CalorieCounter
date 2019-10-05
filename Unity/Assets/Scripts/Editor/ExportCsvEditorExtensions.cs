using CalorieCounter.ScaleEntries;
using CalorieCounter.Utilities;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    public static class ExportCsvEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/Export CSV/";

        [MenuItem(MenuItemDirectory + "Scale Entries")]
        public static void ExportCsvScaleEntries()
        {
            if (Application.isPlaying)
                return;

            var scaleEntries = JsonConverter.Import<SortedList<DateTime, ScaleEntry>>(GlobalPaths.JsonScaleEntriesFileName, true);
            var records = new List<ScaleEntryRecord>();
            foreach (var scaleEntry in scaleEntries.Values)
            {
                records.Add(new ScaleEntryRecord(scaleEntry));
            }

            var filePath = GetFullCsvFilePath(GlobalPaths.CsvScaleEntriesFileName);
            File.WriteAllText(filePath, string.Empty);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(records);
            }
            Debug.Log($"New records written to {filePath}");
        }

        private static string GetFullCsvFilePath(string filePath)
        {
            string fullCsvFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.CsvDirectoryName, filePath));
            string fullCsvFilePathDir = Path.GetDirectoryName(fullCsvFilePath);

            if (!Directory.Exists(fullCsvFilePathDir))
            {
                Directory.CreateDirectory(fullCsvFilePathDir);
            }
            return fullCsvFilePath;
        }
    }
}