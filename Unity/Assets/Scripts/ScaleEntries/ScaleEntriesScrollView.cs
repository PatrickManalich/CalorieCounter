using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries
{

    public class ScaleEntriesScrollView : AbstractScrollView
    {
        public SortedList<DateTime, ScaleEntry> ScaleEntries { get; private set; } = new SortedList<DateTime, ScaleEntry>();

        [SerializeField]
        private ScaleEntryInputFields _scaleEntriesInputFields = default;

        public void AddScaleEntryFromInputFields()
        {
            AddScaleEntry(_scaleEntriesInputFields.GetScaleEntryFromInputFields());
        }

        public void AddScaleEntry(ScaleEntry scaleEntry)
        {
            ScaleEntries.Add(scaleEntry.dateTime, scaleEntry);

            GameObject dateText = InstantiateScrollViewText();
            GameObject weightText = InstantiateScrollViewText();
            GameObject bodyFatText = InstantiateScrollViewText();
            GameObject bodyWaterText = InstantiateScrollViewText();
            GameObject muscleMassText = InstantiateScrollViewText();
            GameObject boneMassText = InstantiateScrollViewText();
            GameObject bmiText = InstantiateScrollViewText();

            int siblingStartIndex = ScaleEntries.IndexOfKey(scaleEntry.dateTime) * _content.constraintCount;
            dateText.transform.SetSiblingIndex(siblingStartIndex);
            weightText.transform.SetSiblingIndex(siblingStartIndex + 1);
            bodyFatText.transform.SetSiblingIndex(siblingStartIndex + 2);
            bodyWaterText.transform.SetSiblingIndex(siblingStartIndex + 3);
            muscleMassText.transform.SetSiblingIndex(siblingStartIndex + 4);
            boneMassText.transform.SetSiblingIndex(siblingStartIndex + 5);
            bmiText.transform.SetSiblingIndex(siblingStartIndex + 6);

            dateText.GetComponent<TextMeshProUGUI>().text = scaleEntry.dateTime.ToShortDateString();
            weightText.GetComponent<TextMeshProUGUI>().text = scaleEntry.weight.ToString();
            bodyFatText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bodyFat.ToString();
            bodyWaterText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bodyWater.ToString();
            muscleMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.muscleMass.ToString();
            boneMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.boneMass.ToString();
            bmiText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bmi.ToString();

            var percent = 1 - (ScaleEntries.IndexOfKey(scaleEntry.dateTime) / (float)(ScaleEntries.Count - 1));
            ScrollToPercent(percent);
        }

        public override void DeleteRow(int rowIndex)
        {
            ScaleEntries.RemoveAt(rowIndex);
            base.DeleteRow(rowIndex);
        }
    }
}
