using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries
{

    public class ScaleEntriesScrollView : ScrollView
    {
        public SortedList<DateTime, ScaleEntry> ScaleEntries { get; private set; } = new SortedList<DateTime, ScaleEntry>();

        public override void DeleteRow(int rowIndex)
        {
            base.DeleteRow(rowIndex);
            ScaleEntries.RemoveAt(rowIndex);
        }

        public void AddScaleEntry(ScaleEntry scaleEntry)
        {
            ScaleEntries.Add(scaleEntry.dateTime, scaleEntry);

            int siblingStartIndex = ScaleEntries.IndexOfKey(scaleEntry.dateTime) * Content.constraintCount;
            GameObject dateText = InstantiateScrollViewText(siblingStartIndex);
            GameObject weightText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject bodyFatText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject bodyWaterText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject muscleMassText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject boneMassText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject bmiText = InstantiateScrollViewText(++siblingStartIndex);

            dateText.GetComponent<TextMeshProUGUI>().text = scaleEntry.dateTime.ToShortDateString();
            weightText.GetComponent<TextMeshProUGUI>().text = scaleEntry.weight.ToString();
            bodyFatText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bodyFat.ToString();
            bodyWaterText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bodyWater.ToString();
            muscleMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.muscleMass.ToString();
            boneMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.boneMass.ToString();
            bmiText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bmi.ToString();

            var percent = 1 - (ScaleEntries.IndexOfKey(scaleEntry.dateTime) / (float)(ScaleEntries.Count - 1));
            ScrollToPercent(percent);
            InvokeRowChanged();
        }
    }
}
