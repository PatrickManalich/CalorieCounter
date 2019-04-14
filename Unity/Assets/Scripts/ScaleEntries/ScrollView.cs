using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.ScaleEntries {

    public class ScrollView : MonoBehaviour {

        public List<ScaleEntry> ScaleEntries { get; private set; } = new List<ScaleEntry>();

        [SerializeField]
        private GameObject _scrollViewBlankPrefab = default;

        [SerializeField]
        private GameObject _scrollViewInputFieldPrefab = default;

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        private List<TMP_InputField> _inputFields = new List<TMP_InputField>();

        public void AddInputFields(Selectable lastSelectable) {
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
            _inputFields[_inputFields.Count - 1].GetComponent<Tabbable>().NextSelectable = lastSelectable;
        }

        public void DeleteInputFields() {
            for (int i = 0; i < _content.constraintCount; i++) {
                int childIndex = (ScaleEntries.Count * _content.constraintCount) + i;
                Destroy(_content.transform.GetChild(childIndex).gameObject);
            }
            _inputFields.Clear();
        }

        public void AddScaleEntryFromInputFields() {
            ScaleEntry scaleEntry = new ScaleEntry(float.Parse(_inputFields[0].text), float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text),
                float.Parse(_inputFields[3].text), float.Parse(_inputFields[4].text), float.Parse(_inputFields[5].text));

            DeleteInputFields();
            AddScaleEntry(scaleEntry);
        }

        public bool HasInputFields() {
            foreach (Transform child in _content.transform) {
                if (child.GetComponent<TMP_InputField>() != null) {
                    return true;
                }
            }
            return false;
        }

        public bool AllInputFieldsFilled() {
            foreach(var inputField in _inputFields) {
                if(inputField.text == "") {
                    return false;
                }
            }
            return true;
        }

        private void Awake() {
            List<ScaleEntry> importedScaleEntries = JsonUtility.ImportScaleEntries(Application.dataPath);
            foreach(var scaleEntry in importedScaleEntries) {
                AddScaleEntry(scaleEntry);
            }
        }

        private void AddScaleEntry(ScaleEntry scaleEntry) {
            GameObject dateText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject weightText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bodyFatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bodyWaterText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject muscleMassText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject boneMassText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bmiText = Instantiate(_scrollViewTextPrefab, _content.transform);

            dateText.GetComponent<TextMeshProUGUI>().text = scaleEntry.Date.ToString("MM/dd/yyyy");
            weightText.GetComponent<TextMeshProUGUI>().text = scaleEntry.Weight.ToString();
            bodyFatText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BodyFat.ToString();
            bodyWaterText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BodyWater.ToString();
            muscleMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.MuscleMass.ToString();
            boneMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BoneMass.ToString();
            bmiText.GetComponent<TextMeshProUGUI>().text = scaleEntry.Bmi.ToString();

            ScaleEntries.Add(scaleEntry);
        }
    }
}
