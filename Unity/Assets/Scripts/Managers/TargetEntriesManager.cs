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
                _targetEntries = JsonConverter.ImportFile<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName);
                _imported = true;
            }

            var firstDateTimeFound = dateTime;
            var terminationDateTime = dateTime.AddYears(-1);
            while (!_targetEntries.ContainsKey(firstDateTimeFound))
            {
                firstDateTimeFound = firstDateTimeFound.AddDays(-1);
                if (firstDateTimeFound < terminationDateTime)
                    return default;
            }
            return _targetEntries[firstDateTimeFound];
        }

        public void ExportTargetEntries(SortedList<DateTime, TargetEntry> targetEntries) {
            JsonConverter.ExportFile(targetEntries, GlobalPaths.JsonTargetEntriesFileName);
            _imported = false;
            ImportLatestTargetEntry(DateTime.Today);
        }
    }
}
