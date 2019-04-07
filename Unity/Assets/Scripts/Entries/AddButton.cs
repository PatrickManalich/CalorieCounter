using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.Entries {

    public class AddButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView;

        [SerializeField]
        private TextMeshProUGUI _errorText;

        [SerializeField]
        private Button _submitButton;

        [SerializeField]
        private Button _cancelButton;

        public void TryAddingInputFields() {
            if (_scrollView.HasInputFields()) {
                _errorText.text = "Input Fields Already Active";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            _scrollView.AddInputFields();
            _submitButton.gameObject.SetActive(true);
            _cancelButton.gameObject.SetActive(true);
        }

        private void Start() {
            _errorText.gameObject.SetActive(false);
        }
    }
}
