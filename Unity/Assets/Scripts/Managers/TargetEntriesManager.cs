using CalorieCounter.TargetEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class TargetEntriesManager : MonoBehaviour {

        private SortedList<DateTime, TargetEntry> _targetEntries;

        private bool _imported = false;

        public SortedList<DateTime, TargetEntry> ImportTargetEntries()
        {
            if (!_imported)
            {
                _targetEntries = JsonConverter.ImportFile<SortedList<DateTime, TargetEntry>>(GlobalPaths.JsonTargetEntriesFileName);
                _imported = true;
            }
            return _targetEntries;
        }

        public TargetEntry ImportLatestTargetEntry(DateTime dateTime)
        {
            ImportTargetEntries();
            var firstDateTimeFound = dateTime;
            var terminationDateTime = dateTime.AddYears(-1);
            while (!_targetEntries.ContainsKey(firstDateTimeFound))
            {
                firstDateTimeFound = firstDateTimeFound.AddDays(-1);
                if (firstDateTimeFound < terminationDateTime)
                {
                    return null;
                }
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
