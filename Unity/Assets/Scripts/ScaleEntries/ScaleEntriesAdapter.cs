using CalorieCounter.Managers;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntriesAdapter : AbstractAdapter {

        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        public override void Export() {
            GameManager.ScaleEntriesManager.ExportScaleEntries(_scaleEntriesScrollView.ScaleEntries);
        }

        private void Start() {
            foreach (var scaleEntry in GameManager.ScaleEntriesManager.ImportScaleEntries().Values) {
                _scaleEntriesScrollView.AddScaleEntry(scaleEntry);
            }
            _scaleEntriesScrollView.ScrollToTop();
        }
    }
}
