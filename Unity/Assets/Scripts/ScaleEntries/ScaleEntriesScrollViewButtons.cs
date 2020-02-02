using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.ScaleEntries
{
    public class ScaleEntriesScrollViewButtons : MonoBehaviour
    {
        [SerializeField]
        private Button _addButton = default;

        [SerializeField]
        private Button _submitButton = default;

        [SerializeField]
        private Button _cancelButton = default;

        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        [SerializeField]
        private ScaleEntryInputFields _scaleEntryInputFields = default;

        private void Start()
        {
            _addButton.onClick.AddListener(() => OnAddButtonClicked());
            _submitButton.onClick.AddListener(() => OnSubmitButtonClicked());
            _cancelButton.onClick.AddListener(() => OnCancelButtonClicked());
        }

        private void OnDestroy()
        {
            _cancelButton.onClick.RemoveAllListeners();
            _submitButton.onClick.RemoveAllListeners();
            _addButton.onClick.RemoveAllListeners();
        }

        private void OnAddButtonClicked()
        {
            _scaleEntryInputFields.Show();
        }

        private void OnSubmitButtonClicked()
        {
            if (_scaleEntryInputFields.Shown)
            {
                var scaleEntry = _scaleEntryInputFields.ScaleEntry;
                _scaleEntryInputFields.Hide();
                _scaleEntriesScrollView.AddScaleEntry(scaleEntry);
            }
        }

        private void OnCancelButtonClicked()
        {
            if (_scaleEntryInputFields.Shown)
            {
                _scaleEntryInputFields.Hide();
            }
        }

    }
}
