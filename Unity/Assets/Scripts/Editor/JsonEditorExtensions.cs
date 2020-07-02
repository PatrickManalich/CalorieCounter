using CalorieCounter.MealEntries;
using CalorieCounter.MealSources;
using CalorieCounter.ScaleEntries;
using CalorieCounter.TargetEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace CalorieCounter.EditorExtensions
{
    public static class JsonEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/Json/";

        [MenuItem(MenuItemDirectory + "Open Editor JSON")]
        public static void OpenEditorJson()
        {
            Application.OpenURL(JsonConverter.EditorJsonDirectoryPath);
            Debug.Log($"{JsonConverter.EditorJsonDirectoryPath} directory opened");
        }

        [MenuItem(MenuItemDirectory + "Open Release JSON")]
        public static void OpenReleaseJson()
        {
            Application.OpenURL(JsonConverter.ReleaseJsonDirectoryPath);
            Debug.Log($"{JsonConverter.ReleaseJsonDirectoryPath} directory opened");
        }

        [MenuItem(MenuItemDirectory + "Copy Release JSON %&#j")]
        public static void CopyReleaseJson()
        {
            if (Application.isPlaying)
                return;

            var directoryInfoSource = new DirectoryInfo(JsonConverter.ReleaseJsonDirectoryPath);
            var directoryInfoTarget = new DirectoryInfo(JsonConverter.EditorJsonDirectoryPath);

            CopyAll(directoryInfoSource, directoryInfoTarget);
            Debug.Log($"Copied {JsonConverter.ReleaseJsonDirectoryPath} into {JsonConverter.EditorJsonDirectoryPath}");
        }

        [MenuItem(MenuItemDirectory + "JSON Updater")]
        public static void JsonUpdater()
        {
            if (Application.isPlaying)
                return;

            var scaleEntries = JsonConverter.ImportFile<SortedList<DateTime, ScaleEntry>>(GlobalPaths.JsonScaleEntriesFileName);
            JsonConverter.ExportFile(scaleEntries, GlobalPaths.JsonScaleEntriesFileName);

            var targetEntries = JsonConverter.ImportFile<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName);
            JsonConverter.ExportFile(targetEntries, GlobalPaths.JsonTargetEntriesFileName);

            var mealSourcesDictionary = JsonConverter.ImportFile<Dictionary<MealSourceType, Dictionary<string, MealSource>>>(GlobalPaths.JsonMealSourcesFileName);
            JsonConverter.ExportFile(mealSourcesDictionary, GlobalPaths.JsonMealSourcesFileName);

            var mealSourceNamesDictionary = JsonConverter.ImportFile<Dictionary<MealSourceType, Dictionary<string, string>>>(GlobalPaths.JsonMealSourceNamesFileName);
            JsonConverter.ExportFile(mealSourceNamesDictionary, GlobalPaths.JsonMealSourceNamesFileName);

            var dateTime = new DateTime(2019, 1, 1);
            while (dateTime <= DateTime.Today)
            {
                var mealEntryPath = JsonConverter.GetMealEntryPath(dateTime);
                if (JsonConverter.DoesFileExist(mealEntryPath))
                {
                    var mealEntry = JsonConverter.ImportFile<MealEntry>(mealEntryPath);
                    JsonConverter.ExportFile(mealEntry, mealEntryPath);
                }
                dateTime = dateTime.AddDays(1);
            }

            Debug.Log("JSON Updater executed");
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fileInfo in source.GetFiles())
            {
                fileInfo.CopyTo(Path.Combine(target.FullName, fileInfo.Name), true);
            }

            foreach (DirectoryInfo targetSubDirectory in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDirectory = target.CreateSubdirectory(targetSubDirectory.Name);
                CopyAll(targetSubDirectory, nextTargetSubDirectory);
            }
        }
    }
}