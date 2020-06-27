using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries
{
    [RequireComponent(typeof(ScrollView))]
    public class ScaleEntriesScrollView : MonoBehaviour
    {
        public ScrollView ScrollView { get; private set; }

        public SortedList<DateTime, ScaleEntry> ScaleEntries { get; private set; } = new SortedList<DateTime, ScaleEntry>();

        public void AddScaleEntry(ScaleEntry scaleEntry)
        {
            ScaleEntries.Add(scaleEntry.dateTime, scaleEntry);

            int siblingStartIndex = ScaleEntries.IndexOfKey(scaleEntry.dateTime) * ScrollView.Content.constraintCount;
            GameObject dateText = ScrollView.InstantiateScrollViewText(siblingStartIndex);
            GameObject weightText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject bodyFatText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject bodyWaterText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject muscleMassText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject boneMassText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);
            GameObject bmiText = ScrollView.InstantiateScrollViewText(++siblingStartIndex);

            dateText.GetComponent<TextMeshProUGUI>().text = scaleEntry.dateTime.ToShortDateString();
            weightText.GetComponent<TextMeshProUGUI>().text = scaleEntry.weight.ToString();
            bodyFatText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bodyFat.ToString();
            bodyWaterText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bodyWater.ToString();
            muscleMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.muscleMass.ToString();
            boneMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.boneMass.ToString();
            bmiText.GetComponent<TextMeshProUGUI>().text = scaleEntry.bmi.ToString();

            var percent = 1 - (ScaleEntries.IndexOfKey(scaleEntry.dateTime) / (float)(ScaleEntries.Count - 1));
            ScrollView.ScrollToPercent(percent);
            ScrollView.InvokeRowAdded(siblingStartIndex);
        }

        private void Awake()
        {
            ScrollView = GetComponent<ScrollView>();
        }

        private void Start()
        {
            ScrollView.RowRemoved += ScrollView_OnRowRemoved;
        }

        private void OnDestroy()
        {
            ScrollView.RowRemoved -= ScrollView_OnRowRemoved;
        }

        private void ScrollView_OnRowRemoved(object sender, ScrollView.RowChangedEventArgs e)
        {
            ScaleEntries.RemoveAt(e.RowIndex);
        }
    }
}
