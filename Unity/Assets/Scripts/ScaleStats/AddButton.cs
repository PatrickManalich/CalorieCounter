using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleStats {

    public class AddButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView;

        [SerializeField]
        private TextMeshProUGUI _errorText;

        public void TryAddingInputFields() {
            if (_scrollView.HasInputFields()) {
                _errorText.text = "Input Fields Already Active";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            _scrollView.AddInputFields();
        }

        private void Start() {
            _errorText.gameObject.SetActive(false);
        }
    }
}
