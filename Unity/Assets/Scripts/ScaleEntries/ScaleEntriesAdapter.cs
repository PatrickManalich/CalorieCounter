using CalorieCounter.Managers;
using System.Linq;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntriesAdapter : AbstractAdapter {

        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        public override void Export() {
            GameManager.ScaleEntriesManager.ExportScaleEntries(_scaleEntriesScrollView.ScaleEntries);
        }

        public override bool DoDifferencesExist()
        {
            var importedScaleEntries = GameManager.ScaleEntriesManager.ImportScaleEntries();
            var scrollViewScaleEntries = _scaleEntriesScrollView.ScaleEntries;
            var doScaleEntriesDiffer = !importedScaleEntries.SequenceEqual(scrollViewScaleEntries);
            return doScaleEntriesDiffer;
        }

        private void Start() {
            foreach (var scaleEntry in GameManager.ScaleEntriesManager.ImportScaleEntries().Values) {
                _scaleEntriesScrollView.AddScaleEntry(scaleEntry);
            }
            _scaleEntriesScrollView.ScrollView.ScrollToTop();
        }
    }
}
