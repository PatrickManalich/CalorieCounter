using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.Globals;

namespace CalorieCounter.MealEntries.CommonMeals {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class MealSourceDropdown : MonoBehaviour {

        [SerializeField]
        private MealType _mealType = default;

        public MealSource SelectedMealSource { get; private set; }
        
        private TMP_Dropdown _dropdown;
        private List<MealSource> _mealSources;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void RefreshSelectedMealSource(int index) {
            if (_mealSources.Exists(x => x.Name == _optionDataList[index].text)) {
                SelectedMealSource = _mealSources.Find(x => x.Name == _optionDataList[index].text);
            } else {
                SelectedMealSource = default;
            }
        }

        public void ResetDropdown() {
            _dropdown.value = 0;
            SelectedMealSource = default;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();

            if (_mealType == MealType.Small) {
                _mealSources = new List<MealSource>(new SmallMeals().MealSources);
            } else if (_mealType == MealType.Large) {
                _mealSources = new List<MealSource>(new LargeMeals().MealSources);
            } else {
                _mealSources = new List<MealSource>();
            }

            _dropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(""));
            foreach (var meal in _mealSources) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(meal.Name));
            }
            _dropdown.AddOptions(_optionDataList);
            SelectedMealSource = default;
        }
        
    }
}
