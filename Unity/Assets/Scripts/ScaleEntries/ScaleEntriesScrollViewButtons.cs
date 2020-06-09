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
            _scaleEntryInputFields.ValidityChanged += ScaleEntryInputFields_OnValidityChanged;

            _addButton.interactable = true;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void OnDestroy()
        {
            _scaleEntryInputFields.ValidityChanged -= ScaleEntryInputFields_OnValidityChanged;
            _cancelButton.onClick.RemoveListener(CancelButton_OnClick);
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
            _addButton.onClick.RemoveListener(AddButton_OnClick);
        }

        private void AddButton_OnClick()
        {
            _scaleEntryInputFields.Show();
            _addButton.interactable = false;
            _submitButton.interactable = false;
            _cancelButton.interactable = true;
        }

        private void SubmitButton_OnClick()
        {
            if (_scaleEntryInputFields.IsShown)
            {
                var scaleEntry = _scaleEntryInputFields.ScaleEntry;
                _scaleEntryInputFields.Hide();
                _scaleEntriesScrollView.AddScaleEntry(scaleEntry);
            }
            _addButton.interactable = true;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void CancelButton_OnClick()
        {
            if (_scaleEntryInputFields.IsShown)
            {
                _scaleEntryInputFields.Hide();
            }
            _addButton.interactable = true;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void ScaleEntryInputFields_OnValidityChanged()
        {
            _submitButton.interactable = _scaleEntryInputFields.IsValid;
        }
    }
}
