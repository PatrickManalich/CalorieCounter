﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.Entries {

    public class SubmitButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView;

        [SerializeField]
        private Button _cancelButton;

        [SerializeField]
        private TextMeshProUGUI _errorText;

        public void TryAddingEntry() {
            if (!_scrollView.AllInputFieldsFilled()) {
                _errorText.text = "Fill All Input Fields";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            _scrollView.AddEntry();
            gameObject.SetActive(false);
            _cancelButton.gameObject.SetActive(false);
        }        
    }
}