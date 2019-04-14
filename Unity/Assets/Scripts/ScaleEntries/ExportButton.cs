using CalorieCounter.TargetEntries;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;

        private const string ScaleEntriesFileName = @"ScaleEntries.json";
        private const string TargetEntriesFileName = @"TargetEntries.json";

        TargetEntryHandler _targetEntryHandler;

        public void TryExporting() {
            if (_scrollView.HasInputFields()) {
                _errorText.text = "Input Fields Are Active";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            JsonUtility.ExportEntries(_scrollView.ScaleEntries, ScaleEntriesFileName);
            _targetEntryHandler.RefreshTargetEntries();
            JsonUtility.ExportEntries(_targetEntryHandler.TargetEntries, TargetEntriesFileName);
            gameObject.SetActive(false);
        }

        private void Awake() {
            _targetEntryHandler = FindObjectOfType<TargetEntryHandler>();
        }
    }
}
