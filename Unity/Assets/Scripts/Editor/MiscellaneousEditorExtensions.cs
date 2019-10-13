using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using OldScaleEntries = CalorieCounterOld.ScaleEntries;
using NewScaleEntries = CalorieCounter.ScaleEntries;
using OldTargetEntries = CalorieCounterOld.TargetEntries;
using NewTargetEntries = CalorieCounter.TargetEntries;


namespace CalorieCounter.EditorExtensions
{
    public static class MiscellaneousEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/";

        [MenuItem(MenuItemDirectory + "Dirty Save Active Scene %#d")]
        public static void DirtySaveAllOpenScenes()
        {
            if (Application.isPlaying)
                return;

            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetSceneAt(i));
                EditorSceneManager.SaveScene(EditorSceneManager.GetSceneAt(i));
            }
        }

        [MenuItem(MenuItemDirectory + "JSON Updater %#j")]
        public static void JsonUpdater()
        {
            if (Application.isPlaying)
                return;

            var oldScaleEntries = JsonConverter.ImportFile<SortedList<DateTime, OldScaleEntries.ScaleEntry>>(GlobalPaths.JsonScaleEntriesFileName, true);
            var newScaleEntries = new SortedList<DateTime, NewScaleEntries.ScaleEntry>();
            foreach (var oldScaleEntry in oldScaleEntries.Values)
            {
                var newScaleEntry = new NewScaleEntries.ScaleEntry(oldScaleEntry.date, oldScaleEntry.weight, oldScaleEntry.bodyFat,
                    oldScaleEntry.bodyWater, oldScaleEntry.muscleMass, oldScaleEntry.boneMass, oldScaleEntry.bmi);
                newScaleEntries.Add(newScaleEntry.dateTime, newScaleEntry);
            }
            JsonConverter.ExportFile(newScaleEntries, GlobalPaths.JsonScaleEntriesFileName, true);


            var oldTargetEntries = JsonConverter.ImportFile<SortedList<DateTime, OldTargetEntries.TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName, true);
            var newTargetEntries = new SortedList<DateTime, NewTargetEntries.TargetEntry>();
            foreach (var oldTargetEntry in oldTargetEntries.Values)
            {
                var newTargetEntry = new NewTargetEntries.TargetEntry(oldTargetEntry.date, oldTargetEntry.weight);
                newTargetEntries.Add(newTargetEntry.dateTime, newTargetEntry);
            }
            JsonConverter.ExportFile(newTargetEntries, GlobalPaths.JsonTargetEntriesFileName, true);
            Debug.Log("JSON Updater ran successfully");
        }
    }
}