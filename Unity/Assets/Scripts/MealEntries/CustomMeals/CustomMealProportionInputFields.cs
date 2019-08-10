using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries {

    public class CustomMealProportionInputFields : MonoBehaviour {

        [SerializeField]
        private List<TMP_InputField> _inputFields = default;

        [SerializeField]
        private List<GameObject> _blanks = default;

        [SerializeField]
        private ScrollRect _scrollRect = default;

        public void ShowInputFields() {
            for (int i = 0; i < _inputFields.Count + _blanks.Count; i++) {
                if (i == 0 || i >= 2 && i <= 4) {
                    TMP_InputField inputField = i == 0 ? _inputFields[i] : _inputFields[i - 1];
                    inputField.transform.SetParent(_scrollRect.content.transform);
                    inputField.gameObject.SetActive(true);
                    inputField.transform.SetAsLastSibling(); ;
                } else {
                    GameObject blank = i == 1 ? _blanks[i - 1] : _blanks[i - 4];
                    blank.transform.SetParent(_scrollRect.content.transform);
                    blank.SetActive(true);
                    blank.transform.SetAsLastSibling(); ;
                }
            }
            _inputFields.First().ActivateInputField();
            StartCoroutine(ScrollToBottom());
        }

        public void HideInputFields() {
            foreach (var inputField in _inputFields) {
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform);
            }
            foreach (var blank in _blanks) {
                blank.SetActive(false);
                blank.transform.SetParent(transform);
            }
        }

        public void CheckIfInputFieldsAreFilled() {
            foreach (var inputField in _inputFields) {
                if (inputField.text == "") {
                    FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
                    return;
                }
            }
            FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
        }

        public MealProportion GetCustomMealProportionFromInputFields() {
            MealSource customMealSource = MealSource.CreateCustomMealSource(float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text), float.Parse(_inputFields[3].text));
            return new MealProportion(float.Parse(_inputFields[0].text), customMealSource);
        }

        private IEnumerator ScrollToBottom()
        {
            yield return new WaitForEndOfFrame();
            _scrollRect.verticalNormalizedPosition = 0;
        }
    }
}
