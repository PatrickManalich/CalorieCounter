﻿using CalorieCounter.ScaleEntries;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers {

    public class ScaleEntriesManager : MonoBehaviour {

        private SortedList<DateTime, ScaleEntry> _scaleEntries;

        private bool _imported = false;

        public SortedList<DateTime, ScaleEntry> ImportScaleEntries() {
            if (!_imported) {
                _scaleEntries = JsonConverter.Import<SortedList<DateTime, ScaleEntry>>(GlobalPaths.ScaleEntriesFilePath);
                _imported = true;
            }
            return _scaleEntries;
        }

        public void ExportScaleEntries(SortedList<DateTime, ScaleEntry> scaleEntries) {
            JsonConverter.Export(scaleEntries, GlobalPaths.ScaleEntriesFilePath);
            _imported = false;
            ImportScaleEntries();
        }
    }
}
