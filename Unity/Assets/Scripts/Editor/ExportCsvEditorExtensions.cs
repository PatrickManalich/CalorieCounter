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

        [MenuItem(MenuItemDirectory + "Results")]
        public static void ExportCsvResults()
        {
            if (Application.isPlaying)
                return;

            var fullReleaseMealEntriesFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\",
                GlobalPaths.CalorieCounterReleaseDirectoryName, GlobalPaths.ReleaseDirectoryName, GlobalPaths.JsonDirectoryName, GlobalPaths.MealEntriesDirectoryName));
            var directoryInfo = new DirectoryInfo(fullReleaseMealEntriesFilePath);
            var mealEntries = new SortedList<DateTime, MealEntry>();
            foreach(var fileInfo in directoryInfo.GetFiles())
            {
                var mealEntryPath = Path.Combine(GlobalPaths.MealEntriesDirectoryName, fileInfo.Name);
                var mealEntry = JsonConverter.Import<MealEntry>(mealEntryPath, true);
                mealEntries.Add(mealEntry.dateTime, mealEntry);
            }
            var targetEntries = JsonConverter.Import<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName, true);

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