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

            for(int i = 0; i < _content.constraintCount - 1; i++) {
                Instantiate(_scrollViewInputFieldPrefab, _content.transform);
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
