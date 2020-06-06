using CalorieCounter.Managers;
using CalorieCounter.MealEntries;
using CalorieCounter.ScaleEntries;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CalorieCounter.TargetEntries {

    public class TargetEntriesAdapter : AbstractAdapter {

        [SerializeField]
        private Scene _scene = default;

        [DisplayBasedOnEnum("_scene", (int)Scene.MealEntries)]
        [SerializeField]
        private Date _date = default;

        [DisplayBasedOnEnum("_scene", (int)Scene.ScaleEntries)]
        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        public override void Export() {
            if (_scene != Scene.ScaleEntries)
            {
                return;
            }

            GameManager.TargetEntriesManager.ExportTargetEntries(GetScrollViewTargetEntries());
        }

        public override bool DoDifferencesExist()
        {
            var importedTargetEntries = GameManager.TargetEntriesManager.ImportTargetEntries();
            var scrollViewTargetEntries = GetScrollViewTargetEntries();
            var doTargetEntriesDiffer = !importedTargetEntries.SequenceEqual(scrollViewTargetEntries);
            return doTargetEntriesDiffer;
        }

        public TargetEntry GetLatestTargetEntry() {
            if (_scene != Scene.MealEntries) {
                return null;
            }

            return GameManager.TargetEntriesManager.ImportLatestTargetEntry(_date.CurrentDateTime);
        }

        private SortedList<DateTime, TargetEntry> GetScrollViewTargetEntries()
        {
            var targetEntries = new SortedList<DateTime, TargetEntry>();
            foreach (var scaleEntry in _scaleEntriesScrollView.ScaleEntries.Values)
            {
                var targetEntry = new TargetEntry(scaleEntry.dateTime, scaleEntry.weight);
                targetEntries.Add(targetEntry.dateTime, targetEntry);
            }
            return targetEntries;
        }
    }
}
