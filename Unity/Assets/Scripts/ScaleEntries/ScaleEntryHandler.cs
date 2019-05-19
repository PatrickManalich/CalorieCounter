using CalorieCounter.Globals;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntryHandler : MonoBehaviour {

        [SerializeField]
        private ScaleEntriesScrollView _scrollView = default;

        public void ExportScaleEntry() {
            JsonUtility.Export(_scrollView.ScaleEntries, GlobalPaths.ScaleEntriesFilePath);
        }

        private void Start() {
            List<ScaleEntry> importedScaleEntries = JsonUtility.Import<List<ScaleEntry>>(GlobalPaths.ScaleEntriesFilePath);
            foreach (var scaleEntry in importedScaleEntries) {
                _scrollView.AddScaleEntry(scaleEntry);
            }
        }
    }
}
