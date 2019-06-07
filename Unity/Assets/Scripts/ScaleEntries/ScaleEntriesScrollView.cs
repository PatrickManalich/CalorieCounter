using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntriesScrollView : MonoBehaviour {

        public List<ScaleEntry> ScaleEntries { get; private set; } = new List<ScaleEntry>();

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        [SerializeField]
        private ScaleEntryInputFields _scaleEntriesInputFields = default;


        public void AddScaleEntryFromInputFields() {
            AddScaleEntry(_scaleEntriesInputFields.GetScaleEntryFromInputFields());
        }

        public void AddScaleEntry(ScaleEntry scaleEntry) {
            GameObject dateText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject weightText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bodyFatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bodyWaterText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject muscleMassText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject boneMassText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject bmiText = Instantiate(_scrollViewTextPrefab, _content.transform);

            dateText.GetComponent<TextMeshProUGUI>().text = scaleEntry.Date.ToShortDateString();
            weightText.GetComponent<TextMeshProUGUI>().text = scaleEntry.Weight.ToString();
            bodyFatText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BodyFat.ToString();
            bodyWaterText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BodyWater.ToString();
            muscleMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.MuscleMass.ToString();
            boneMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BoneMass.ToString();
            bmiText.GetComponent<TextMeshProUGUI>().text = scaleEntry.Bmi.ToString();

            ScaleEntries.Add(scaleEntry);
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
