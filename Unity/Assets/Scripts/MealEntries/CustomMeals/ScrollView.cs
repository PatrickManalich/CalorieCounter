using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class ScrollView : MonoBehaviour {

        [SerializeField]
        private GameObject _scrollViewBlankPrefab = default;

        [SerializeField]
        private GameObject _scrollViewInputFieldPrefab = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        private List<TMP_InputField> _inputFields = new List<TMP_InputField>();

        public void AddInputFields() {
            GameObject previous = null;
            for (int i = 0; i < _content.constraintCount; i++) {
                if (i == 0) {
                    previous = Instantiate(_scrollViewInputFieldPrefab, _content.transform);
                    previous.GetComponent<TMP_InputField>().ActivateInputField();
                    _inputFields.Add(previous.GetComponent<TMP_InputField>());
                } else if(i >= 2 && i <= 4) {
                    GameObject current = Instantiate(_scrollViewInputFieldPrefab, _content.transform);
                    previous.GetComponent<Tabbable>().NextSelectable = current.GetComponent<Selectable>();
                    previous = current;
                    _inputFields.Add(previous.GetComponent<TMP_InputField>());
                } else {
                    Instantiate(_scrollViewBlankPrefab, _content.transform);
                }
            }
        }

        public bool HasInputFields() {
            foreach (Transform child in _content.transform) {
                if (child.GetComponent<TMP_InputField>() != null) {
                    return true;
                }
            }
            return false;
        }
    }
}
