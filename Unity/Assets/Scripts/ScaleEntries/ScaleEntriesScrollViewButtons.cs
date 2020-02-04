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
            _addButton.onClick.AddListener(AddButton_OnClick);
            _submitButton.onClick.AddListener(SubmitButton_OnClick);
            _cancelButton.onClick.AddListener(CancelButton_OnClick);
        }

        private void OnDestroy()
        {
            _cancelButton.onClick.RemoveListener(CancelButton_OnClick);
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
            _addButton.onClick.RemoveListener(AddButton_OnClick);
        }

        private void AddButton_OnClick()
        {
            _scaleEntryInputFields.Show();
        }

        private void SubmitButton_OnClick()
        {
            if (_scaleEntryInputFields.IsShown)
            {
                var scaleEntry = _scaleEntryInputFields.ScaleEntry;
                _scaleEntryInputFields.Hide();
                _scaleEntriesScrollView.AddScaleEntry(scaleEntry);
            }
        }

        private void CancelButton_OnClick()
        {
            if (_scaleEntryInputFields.IsShown)
            {
                _scaleEntryInputFields.Hide();
            }
        }

    }
}
