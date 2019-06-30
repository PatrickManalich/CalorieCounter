using CalorieCounter.TargetEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class TargetEntriesManager : MonoBehaviour {

        private List<TargetEntry> _targetEntries = new List<TargetEntry>();
        private bool _imported = false;

        public void ImportTargetEntries() {
            if (!_imported) {
                _targetEntries = JsonConverter.Import<List<TargetEntry>>(GlobalPaths.TargetEntriesFilePath);
                _imported = true;
            }
        }

        public void ExportTargetEntries(List<TargetEntry> targetEntries) {
            JsonConverter.Export(targetEntries, GlobalPaths.TargetEntriesFilePath);
        }

        public TargetEntry GetLatestTargetEntry(DateTime date) {
            TargetEntry latestTargetEntry = default;
            foreach (var targetEntry in _targetEntries) {
                if (targetEntry.Date <= date) {
                    latestTargetEntry = targetEntry;
                } else {
                    break;
                }
            }
            return latestTargetEntry;
        }
    }
}
