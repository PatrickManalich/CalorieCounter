using CalorieCounter.TargetEntries;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class TargetEntriesManager : MonoBehaviour {

        public List<TargetEntry> TargetEntries = new List<TargetEntry>();

        private bool _imported = false;

        public void ImportTargetEntries() {
            if (!_imported) {
                TargetEntries = JsonConverter.Import<List<TargetEntry>>(GlobalPaths.TargetEntriesFilePath);
                _imported = true;
            }
        }

        public void ExportTargetEntries(List<TargetEntry> targetEntries) {
            JsonConverter.Export(targetEntries, GlobalPaths.TargetEntriesFilePath);
        }
    }
}
