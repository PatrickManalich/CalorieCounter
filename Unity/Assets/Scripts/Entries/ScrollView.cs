using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.Entries {

    public class ScrollView : MonoBehaviour {

        [SerializeField]
        private GameObject _scrollViewBlankPrefab;

        [SerializeField]
        private GameObject _scrollViewInputFieldPrefab;

        [SerializeField]
        private GridLayoutGroup _content;

        private List<TMP_InputField> _inputFields = new List<TMP_InputField>(); 

        public void AddInputFields() {
            Instantiate(_scrollViewBlankPrefab, _content.transform);

            GameObject previous = null;
            for(int i = 0; i < _content.constraintCount - 1; i++) {
                if(i == 0) {
                    previous = Instantiate(_scrollViewInputFieldPrefab, _content.transform);
                    previous.GetComponent<TMP_InputField>().ActivateInputField();
                } else {
                    GameObject current = Instantiate(_scrollViewInputFieldPrefab, _content.transform);
                    previous.GetComponent<Tabbable>().NextSelectable = current.GetComponent<Selectable>();
                    previous = current;
                }
                _inputFields.Add(previous.GetComponent<TMP_InputField>());
            }
        }

        public void AddEntry() {
            Debug.Log("Adding!");
        }

        public bool HasInputFields() {
            return _inputFields.Count > 0;
        }

        public bool AllInputFieldsFilled() {
            foreach(var inputField in _inputFields) {
                if(inputField.text == "") {
                    return false;
                }
            }
            return true;
        }
    }
}
