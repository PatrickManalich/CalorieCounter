using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.ScaleStats {

    public class ScrollView : MonoBehaviour {

        [SerializeField]
        private GameObject _scrollViewBlankPrefab;

        [SerializeField]
        private GameObject _scrollViewInputFieldPrefab;

        [SerializeField]
        private GridLayoutGroup _content;

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
            }
        }

        public bool HasInputFields() {
            foreach(Transform child in _content.transform) {
                if(child.GetComponent<TMP_InputField>() != null) {
                    return true;
                }
            }
            return false;
        }
    }
}
