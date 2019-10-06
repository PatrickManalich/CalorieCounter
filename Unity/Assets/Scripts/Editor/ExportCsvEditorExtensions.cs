using CalorieCounter.ScaleEntries;
using CalorieCounter.TargetEntries;
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
            WriteRecords(records, GlobalPaths.CsvScaleEntriesFileName);
        }

        [MenuItem(MenuItemDirectory + "Target Entries")]
        public static void ExportCsvTargetEntries()
        {
            if (Application.isPlaying)
                return;

            var targetEntries = JsonConverter.Import<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName, true);
            var records = new List<TargetEntryRecord>();
            foreach (var targetEntry in targetEntries.Values)
            {
                records.Add(new TargetEntryRecord(targetEntry));
            }
            WriteRecords(records, GlobalPaths.CsvTargetEntriesFileName);
        }

        private static void WriteRecords<T>(IEnumerable<T> records, string fileName){
            var filePath = GetFullCsvFilePath(fileName);
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