using CalorieCounter.Globals;
using CalorieCounter.TargetEntries;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;

        TargetEntryHandler _targetEntryHandler;

        public void TryExporting() {
            if (_scrollView.HasInputFields()) {
                _errorText.text = "Input Fields Are Active";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            JsonUtility.Export(_scrollView.ScaleEntries, GlobalPaths.ScaleEntriesFilePath);
            _targetEntryHandler.RefreshTargetEntries();
            JsonUtility.Export(_targetEntryHandler.TargetEntries, GlobalPaths.TargetEntriesFilePath);
            gameObject.SetActive(false);
        }

        private void Awake() {
            _targetEntryHandler = FindObjectOfType<TargetEntryHandler>();
        }
    }
}
