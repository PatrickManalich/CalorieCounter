﻿using CalorieCounter.Managers;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntriesAdapter : MonoBehaviour {

        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        public void ExportScaleEntry() {
            GameManager.ScaleEntriesManager.ExportScaleEntries(_scaleEntriesScrollView.ScaleEntries);
        }

        private void Start() {
            GameManager.ScaleEntriesManager.ImportScaleEntries();
            foreach (var scaleEntry in GameManager.ScaleEntriesManager.ScaleEntries) {
                _scaleEntriesScrollView.AddScaleEntry(scaleEntry);
            }
        }
    }
}