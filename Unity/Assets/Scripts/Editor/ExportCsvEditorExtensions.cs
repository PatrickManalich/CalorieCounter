using CalorieCounter.MealEntries;
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

        [MenuItem(MenuItemDirectory + "Scale Entries")]
        public static void ExportCsvScaleEntries()
        {
            if (Application.isPlaying)
                return;

            var scaleEntries = JsonConverter.ImportFile<SortedList<DateTime, ScaleEntry>>(GlobalPaths.JsonScaleEntriesFileName, true);
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

            var targetEntries = JsonConverter.ImportFile<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName, true);
            var records = new List<TargetEntryRecord>();
            foreach (var targetEntry in targetEntries.Values)
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

            var mealEntries = JsonConverter.ImportMealEntries(true);
            var targetEntries = JsonConverter.ImportFile<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName, true);
            var records = new List<ResultRecord>();
            foreach (var mealEntry in mealEntries.Values)
            {
                if (mealEntry.dayType != DayType.Rest && mealEntry.dayType != DayType.Training)
                    continue;

                var firstDateTimeFound = mealEntry.dateTime;
                var terminationDateTime = mealEntry.dateTime.AddYears(-1);
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
            var filePath = GetCsvFilePath(fileName);
            File.WriteAllText(filePath, string.Empty);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(records);
            }
            Debug.Log($"New records written to {filePath}");
        }

        private static string GetCsvFilePath(string filePath)
        {
            string csvFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.CsvDirectoryName, filePath));
            string csvFilePathDir = Path.GetDirectoryName(csvFilePath);

            if (!Directory.Exists(csvFilePathDir))
            {
                Directory.CreateDirectory(csvFilePathDir);
            }
            return csvFilePath;
        }
    }
}