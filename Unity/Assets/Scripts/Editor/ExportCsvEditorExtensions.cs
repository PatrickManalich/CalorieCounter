using CalorieCounter.ScaleEntries;
using CalorieCounter.TargetEntries;
using CalorieCounter.Utilities;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    public static class ExportCsvEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/Export CSV/";
        private const int ExportYear = 2020;

        [MenuItem(MenuItemDirectory + "Scale Entries")]
        public static void ExportCsvScaleEntries()
        {
            if (Application.isPlaying)
                return;

            var scaleEntries = JsonConverter.ImportFile<SortedList<DateTime, ScaleEntry>>(GlobalPaths.JsonScaleEntriesFileName);
            var records = new List<ScaleEntryRecord>();
            foreach (var scaleEntry in scaleEntries.Values.Where(s => s.DateTime.Year == ExportYear))
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

            var targetEntries = JsonConverter.ImportFile<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName);
            var records = new List<TargetEntryRecord>();
            foreach (var targetEntry in targetEntries.Values.Where(t => t.DateTime.Year == ExportYear))
            {
                records.Add(new TargetEntryRecord(targetEntry));
            }
            WriteRecords(records, GlobalPaths.CsvTargetEntriesFileName);
        }

        [MenuItem(MenuItemDirectory + "Results")]
        public static void ExportCsvResults()
        {
            if (Application.isPlaying)
                return;

            var mealEntries = JsonConverter.ImportMealEntries();
            var targetEntries = JsonConverter.ImportFile<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName);
            var records = new List<ResultRecord>();
            foreach (var mealEntry in mealEntries.Values.Where(m => m.DateTime.Year == ExportYear))
            {
                if (mealEntry.DayType != DayType.Rest && mealEntry.DayType != DayType.Training)
                    continue;

                var firstDateTimeFound = mealEntry.DateTime;
                var terminationDateTime = mealEntry.DateTime.AddYears(-1);
                while (!targetEntries.ContainsKey(firstDateTimeFound))
                {
                    firstDateTimeFound = firstDateTimeFound.AddDays(-1);
                    if (firstDateTimeFound < terminationDateTime)
                    {
                        Debug.LogError($"No target entry found for meal entry {mealEntry}");
                        return;
                    }
                }
                var targetEntry = targetEntries[firstDateTimeFound];
                records.Add(new ResultRecord(mealEntry, targetEntry));
            }
            WriteRecords(records, GlobalPaths.CsvResultsFileName);
        }

        [MenuItem(MenuItemDirectory + "All")]
        public static void ExportCsvAll()
        {
            if (Application.isPlaying)
                return;

            ExportCsvScaleEntries();
            ExportCsvTargetEntries();
            ExportCsvResults();
        }

        private static void WriteRecords<T>(IEnumerable<T> records, string fileName){
            var csvFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.CsvDirectoryName, ExportYear.ToString(), fileName));
            var csvFilePathDirectory = Path.GetDirectoryName(csvFilePath);

            if (!Directory.Exists(csvFilePathDirectory))
            {
                Directory.CreateDirectory(csvFilePathDirectory);
            }
            File.WriteAllText(csvFilePath, string.Empty);

            using (var writer = new StreamWriter(csvFilePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(records);
            }
            Debug.Log($"New records written to {csvFilePath}");
        }
    }
}