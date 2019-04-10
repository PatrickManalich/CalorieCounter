using TMPro;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace CalorieCounter.ScaleEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView;

        [SerializeField]
        private TextMeshProUGUI _errorText;

        private const string localJsonDirPath = @"../../Json";
        private const string scaleEntriesFileName = @"ScaleEntries.json";

        public void TryExporting() {
            if (_scrollView.HasInputFields()) {
                _errorText.text = "Input Fields Are Active";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);

            string jsonDirPath = Path.GetFullPath(Path.Combine(Application.dataPath, localJsonDirPath));
            if (!Directory.Exists(jsonDirPath)) {
                Directory.CreateDirectory(jsonDirPath);
            }
            
            using(StreamWriter file = File.CreateText(Path.Combine(jsonDirPath, scaleEntriesFileName))) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _scrollView.Entries);
            }
        }
    }
}
