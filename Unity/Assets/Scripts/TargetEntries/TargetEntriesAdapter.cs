using CalorieCounter.Managers;
using CalorieCounter.MealEntries;
using CalorieCounter.ScaleEntries;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.TargetEntries {

    public class TargetEntriesAdapter : MonoBehaviour {

        public TargetEntry LatestTargetEntry { get; private set; }

        [SerializeField]
        private Scene _scene = default;

        [DisplayBasedOnEnum("_scene", (int)Scene.MealEntries)]
        [SerializeField]
        private Date _date = default;

        public void Refresh() {
            if (_scene == Scene.MealEntries) {
                LatestTargetEntry = GameManager.TargetEntriesManager.GetLatestTargetEntry(_date.CurrentDate);
            }
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
            Refresh();
        }
    }
}
