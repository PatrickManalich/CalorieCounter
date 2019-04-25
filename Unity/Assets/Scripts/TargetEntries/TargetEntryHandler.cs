using CalorieCounter.ScaleEntries;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.TargetEntries {

    public class TargetEntryHandler : MonoBehaviour {

        public List<TargetEntry> TargetEntries { get; private set; } = new List<TargetEntry>();

        private const string TargetEntriesFileName = @"TargetEntries.json";

        private ScrollView _scrollView;

        public void RefreshTargetEntries() {
            TargetEntries.Clear();
            foreach(var scaleEntry in _scrollView.ScaleEntries) {
                TargetEntries.Add(new TargetEntry(scaleEntry.Date, scaleEntry.Weight));
            }
        }

        private void Awake() {
            TargetEntries = JsonUtility.Import<List<TargetEntry>>(TargetEntriesFileName);
            _scrollView = FindObjectOfType<ScrollView>();
        }
    }
}
