using CalorieCounter.ScaleEntries;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class ScaleEntriesManager : MonoBehaviour {

        public List<ScaleEntry> ScaleEntries { get; private set; }

        private bool _imported = false;

        public void ImportScaleEntries() {
            if (!_imported) {
                ScaleEntries = JsonConverter.Import<List<ScaleEntry>>(GlobalPaths.ScaleEntriesFilePath);
                _imported = true;
            }
        }

        public void ExportScaleEntries(List<ScaleEntry> scaleEntries) {
            JsonConverter.Export(scaleEntries, GlobalPaths.ScaleEntriesFilePath);
        }
    }
}
