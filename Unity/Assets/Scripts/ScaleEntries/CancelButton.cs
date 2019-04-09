using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.ScaleEntries {

    public class CancelButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView;

        [SerializeField]
        private Button _submitButton;

        [SerializeField]
        private TextMeshProUGUI _errorText;

        public void CancelAddingEntry() {
            _errorText.gameObject.SetActive(false);
            _scrollView.DeleteInputFields();
            gameObject.SetActive(false);
            _submitButton.gameObject.SetActive(false);
        }
    }
}
