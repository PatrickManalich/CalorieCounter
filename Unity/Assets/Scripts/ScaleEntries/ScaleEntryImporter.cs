using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntryImporter : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView = default;

        private const string ScaleEntriesFilePath = @"ScaleEntries.json";

        private void Start() {
            List<ScaleEntry> importedScaleEntries = JsonUtility.Import<List<ScaleEntry>>(ScaleEntriesFilePath);
            foreach (var scaleEntry in importedScaleEntries) {
                _scrollView.AddScaleEntry(scaleEntry);
            }
        }
    }
}
