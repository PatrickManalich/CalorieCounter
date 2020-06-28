using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries
{
    [RequireComponent(typeof(ScrollViewAssistant))]
    public class ScaleEntriesScrollView : MonoBehaviour
    {
        public ScrollViewAssistant ScrollViewAssistant { get; private set; }

        public SortedList<DateTime, ScaleEntry> ScaleEntries { get; private set; } = new SortedList<DateTime, ScaleEntry>();

        public void AddScaleEntry(ScaleEntry scaleEntry)
        {
            ScaleEntries.Add(scaleEntry.DateTime, scaleEntry);

            int siblingStartIndex = ScaleEntries.IndexOfKey(scaleEntry.DateTime) * ScrollViewAssistant.Content.constraintCount;
            GameObject dateText = ScrollViewAssistant.InstantiateScrollViewText(siblingStartIndex);
            GameObject weightText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject bodyFatText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject bodyWaterText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject muscleMassText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject boneMassText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject bmiText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);

            dateText.GetComponent<TextMeshProUGUI>().text = scaleEntry.DateTime.ToShortDateString();
            weightText.GetComponent<TextMeshProUGUI>().text = scaleEntry.Weight.ToString();
            bodyFatText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BodyFat.ToString();
            bodyWaterText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BodyWater.ToString();
            muscleMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.MuscleMass.ToString();
            boneMassText.GetComponent<TextMeshProUGUI>().text = scaleEntry.BoneMass.ToString();
            bmiText.GetComponent<TextMeshProUGUI>().text = scaleEntry.Bmi.ToString();

            var percent = 1 - (ScaleEntries.IndexOfKey(scaleEntry.DateTime) / (float)(ScaleEntries.Count - 1));
            ScrollViewAssistant.ScrollToPercent(percent);
            ScrollViewAssistant.InvokeRowAdded(siblingStartIndex);
        }

        private void Awake()
        {
            ScrollViewAssistant = GetComponent<ScrollViewAssistant>();
        }

        private void Start()
        {
            ScrollViewAssistant.RowRemoved += ScrollViewAssistant_OnRowRemoved;
        }

        private void OnDestroy()
        {
            ScrollViewAssistant.RowRemoved -= ScrollViewAssistant_OnRowRemoved;
        }

        private void ScrollViewAssistant_OnRowRemoved(object sender, ScrollViewAssistant.RowChangedEventArgs e)
        {
            ScaleEntries.RemoveAt(e.RowIndex);
        }
    }
}
