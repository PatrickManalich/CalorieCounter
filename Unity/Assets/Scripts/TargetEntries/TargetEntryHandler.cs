using CalorieCounter.Globals;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.TargetEntries {

    public class TargetEntryHandler : MonoBehaviour {

        private List<TargetEntry> _targetEntries = new List<TargetEntry>();

        public void AddTargetEntry(DateTime date, float weight) {
            _targetEntries.Add(new TargetEntry(date, weight));
        }

        public void ClearTargetEntries() {
            _targetEntries.Clear();
        }

        public TargetEntry GetLatestTargetEntry(DateTime date) {
            TargetEntry latestTargetEntry = default;
            foreach(var targetEntry in _targetEntries) {
                if (targetEntry.Date <= date) {
                    latestTargetEntry = targetEntry;
                } else {
                    break;
                }
            }
            return latestTargetEntry;
        }

        public void ExportTargetEntry() {
            JsonUtility.Export(_targetEntries, GlobalPaths.TargetEntriesFilePath);
        }

        private void Awake() {
            _targetEntries = JsonUtility.Import<List<TargetEntry>>(GlobalPaths.TargetEntriesFilePath);
        }
    }
}
