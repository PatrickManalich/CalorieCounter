using CalorieCounter.TargetEntries;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private ScaleEntryHandler _scaleEntryHandler = default;

        [SerializeField]
        private TargetEntryHandler _targetEntryHandler = default;

        [SerializeField]
        private ScrollView _scrollView = default;

        public void Export() {
            _scaleEntryHandler.ExportScaleEntry();
            _targetEntryHandler.ClearTargetEntries();
            foreach (var scaleEntry in _scrollView.ScaleEntries) {
                _targetEntryHandler.AddTargetEntry(scaleEntry.Date, scaleEntry.Weight);
            }
            _targetEntryHandler.ExportTargetEntry();
            gameObject.SetActive(false);
        }
    }
}
