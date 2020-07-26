using System;
using System.Collections.Generic;

namespace CalorieCounter.ScaleEntries
{
    public class ScaleEntriesScrollView : ScrollView
    {
        public SortedList<DateTime, ScaleEntry> ScaleEntries { get; private set; } = new SortedList<DateTime, ScaleEntry>();

        public void AddScaleEntry(ScaleEntry scaleEntry, bool scrollToPercent = true)
        {
            ScaleEntries.Add(scaleEntry.DateTime, scaleEntry);

            int siblingStartIndex = ScaleEntries.IndexOfKey(scaleEntry.DateTime) * ScrollViewAssistant.Content.constraintCount;
            ScrollViewAssistant.InstantiateScrollViewText(siblingStartIndex, scaleEntry.DateTime.ToShortDateString());
            ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex, scaleEntry.Weight.ToString());
            ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex, scaleEntry.BodyFat.ToString());
            ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex, scaleEntry.BodyWater.ToString());
            ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex, scaleEntry.MuscleMass.ToString());
            ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex, scaleEntry.BoneMass.ToString());
            ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex, scaleEntry.Bmi.ToString());

            if (scrollToPercent)
            {
                var percent = 1 - (ScaleEntries.IndexOfKey(scaleEntry.DateTime) / (float)(ScaleEntries.Count - 1));
                ScrollViewAssistant.ScrollToPercent(percent);
            }
            InvokeRowAdded(siblingStartIndex);
        }

        public void RemoveRow(int rowIndex)
        {
            ScaleEntries.RemoveAt(rowIndex);
            ScrollViewAssistant.RemoveRow(rowIndex);
            InvokeRowRemoved(rowIndex);
        }
    }
}
