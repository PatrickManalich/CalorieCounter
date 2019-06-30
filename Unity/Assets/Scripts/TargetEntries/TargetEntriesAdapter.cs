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

        public TargetEntry GetLatestTargetEntry() {
            if (_scene != Scene.MealEntries) {
                return default;
            }

            TargetEntry latestTargetEntry = default;
            foreach (var targetEntry in GameManager.TargetEntriesManager.TargetEntries) {
                if (targetEntry.Date <= _date.CurrentDate) {
                    latestTargetEntry = targetEntry;
                } else {
                    break;
                }
            }
            return latestTargetEntry;
        }

        public void ExportTargetEntries(ScaleEntriesScrollView scrollView) {
            List<TargetEntry> targetEntries = new List<TargetEntry>();
            foreach (var scaleEntry in scrollView.ScaleEntries) {
                targetEntries.Add(new TargetEntry(scaleEntry.Date, scaleEntry.Weight));
            }
            GameManager.TargetEntriesManager.ExportTargetEntries(targetEntries);
        }

        private void Awake() {
            GameManager.TargetEntriesManager.ImportTargetEntries();
        }
    }
}
