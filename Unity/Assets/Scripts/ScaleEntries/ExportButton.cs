using CalorieCounter.TargetEntries;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private ScaleEntryHandler _scaleEntryHandler = default;

        [SerializeField]
        private TargetEntryHandler _targetEntryHandler = default;

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;


        public void TryExporting() {
            if (_scrollView.HasInputFields()) {
                _errorText.text = "Input Fields Are Active";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
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
