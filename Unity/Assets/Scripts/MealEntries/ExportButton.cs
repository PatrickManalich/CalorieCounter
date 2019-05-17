using CalorieCounter.Globals;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class ExportButton : MonoBehaviour {

        [SerializeField]
        private MealEntryHandler _mealEntryHandler = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;

        public void TryExporting() {
            if (_dayTypeDropdown.DayType == DayTypes.None) {
                _errorText.text = "Select Valid Meal Type";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            _mealEntryHandler.ExportMealEntry();
            gameObject.SetActive(false);
        }
    }
}
