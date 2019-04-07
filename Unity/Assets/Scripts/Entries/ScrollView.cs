﻿using System.Collections.Generic;
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
        private GameObject _scrollViewTextPrefab;

        [SerializeField]
        private GridLayoutGroup _content;

        private List<TMP_InputField> _inputFields = new List<TMP_InputField>();

        private List<Entry> _entries = new List<Entry>();

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

        public void DeleteInputFields() {
            for (int i = 0; i < _content.constraintCount; i++) {
                int childIndex = (_entries.Count * _content.constraintCount) + i;
                Destroy(_content.transform.GetChild(childIndex).gameObject);
            }
            _inputFields.Clear();
        }

        public void AddEntry() {
            GameObject dateText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject weightText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bodyFatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bodyWaterText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject muscleMassText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject boneMassText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bmiText = Instantiate(_scrollViewTextPrefab, _content.transform);

            Entry entry = new Entry(float.Parse(_inputFields[0].text), float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text),
                float.Parse(_inputFields[3].text), float.Parse(_inputFields[4].text), float.Parse(_inputFields[5].text));

            DeleteInputFields();

            dateText.GetComponent<TextMeshProUGUI>().text = entry.Date.ToString("MM/dd/yyyy");
            weightText.GetComponent<TextMeshProUGUI>().text = entry.Weight.ToString();
            bodyFatText.GetComponent<TextMeshProUGUI>().text = entry.BodyFat.ToString();
            bodyWaterText.GetComponent<TextMeshProUGUI>().text = entry.BodyWater.ToString();
            muscleMassText.GetComponent<TextMeshProUGUI>().text = entry.MuscleMass.ToString();
            boneMassText.GetComponent<TextMeshProUGUI>().text = entry.BoneMass.ToString();
            bmiText.GetComponent<TextMeshProUGUI>().text = entry.Bmi.ToString();

            _entries.Add(entry);
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
    }
}
