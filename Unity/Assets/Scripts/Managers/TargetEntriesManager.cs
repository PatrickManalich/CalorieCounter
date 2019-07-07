using CalorieCounter.TargetEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class TargetEntriesManager : MonoBehaviour {

        public List<TargetEntry> _targetEntries;

        private bool _imported = false;

        public TargetEntry ImportLatestTargetEntry(DateTime dateTime) {
            if (!_imported) {
                _targetEntries = JsonConverter.Import<List<TargetEntry>>(GlobalPaths.TargetEntriesFilePath);
                _imported = true;
            }

            TargetEntry latestTargetEntry = default;
            foreach (var targetEntry in _targetEntries)
            {
                if (targetEntry.Date <= dateTime)
                {
                    latestTargetEntry = targetEntry;
                }
                else
                {
                    break;
                }
            }
            return latestTargetEntry;
        }

        public void ExportTargetEntries(List<TargetEntry> targetEntries) {
            JsonConverter.Export(targetEntries, GlobalPaths.TargetEntriesFilePath);
        }
    }
}
