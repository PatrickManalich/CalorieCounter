using CalorieCounter.Managers;
using CalorieCounter.MealEntries;
using CalorieCounter.ScaleEntries;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.TargetEntries {

    public class TargetEntriesAdapter : MonoBehaviour {

        [SerializeField]
        private Scene _scene = default;

        [DisplayBasedOnEnum("_scene", (int)Scene.MealEntries)]
        [SerializeField]
        private Date _date = default;

        [DisplayBasedOnEnum("_scene", (int)Scene.ScaleEntries)]
        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        public TargetEntry GetLatestTargetEntry() {
            if (_scene != Scene.MealEntries) {
                return default;
            }

            return GameManager.TargetEntriesManager.ImportLatestTargetEntry(_date.CurrentDate);
        }

        public void ExportTargetEntries() {
            if (_scene != Scene.ScaleEntries)
            {
                return;
            }

            List<TargetEntry> targetEntries = new List<TargetEntry>();
            foreach (var scaleEntry in _scaleEntriesScrollView.ScaleEntries) {
                targetEntries.Add(new TargetEntry(scaleEntry.Date, scaleEntry.Weight));
            }
            GameManager.TargetEntriesManager.ExportTargetEntries(targetEntries);
        }
    }
}
