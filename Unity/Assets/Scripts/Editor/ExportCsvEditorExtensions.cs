﻿using CalorieCounter.ScaleEntries;
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
            string csvFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.CsvDirectoryName, fileName));
            string csvFilePathDirectory = Path.GetDirectoryName(csvFilePath);

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