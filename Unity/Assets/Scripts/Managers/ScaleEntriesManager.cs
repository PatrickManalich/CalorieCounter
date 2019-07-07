using CalorieCounter.ScaleEntries;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class ScaleEntriesManager : MonoBehaviour {

        private List<ScaleEntry> _scaleEntries;

        private bool _imported = false;

        public List<ScaleEntry> ImportScaleEntries() {
            if (!_imported) {
                _scaleEntries = JsonConverter.Import<List<ScaleEntry>>(GlobalPaths.ScaleEntriesFilePath);
                _imported = true;
            }
            return _scaleEntries;
        }

        public void ExportScaleEntries(List<ScaleEntry> scaleEntries) {
            JsonConverter.Export(scaleEntries, GlobalPaths.ScaleEntriesFilePath);
        }
    }
}
