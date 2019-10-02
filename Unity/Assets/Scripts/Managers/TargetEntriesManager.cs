using CalorieCounter.TargetEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class TargetEntriesManager : MonoBehaviour {

        public SortedList<DateTime, TargetEntry> _targetEntries;

        private bool _imported = false;

        public TargetEntry ImportLatestTargetEntry(DateTime dateTime) {
            if (!_imported) {
                _targetEntries = JsonConverter.Import<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName);
                _imported = true;
            }

            var latestTargetEntryKey = dateTime;
            var terminationDate = dateTime.AddYears(-1);
            while (!_targetEntries.ContainsKey(latestTargetEntryKey))
            {
                latestTargetEntryKey = latestTargetEntryKey.AddDays(-1);
                if (latestTargetEntryKey < terminationDate)
                    return default;
            }
            return _targetEntries[latestTargetEntryKey];
        }

        public void ExportTargetEntries(SortedList<DateTime, TargetEntry> targetEntries) {
            JsonConverter.Export(targetEntries, GlobalPaths.JsonTargetEntriesFileName);
            _imported = false;
            ImportLatestTargetEntry(DateTime.Today);
        }
    }
}
