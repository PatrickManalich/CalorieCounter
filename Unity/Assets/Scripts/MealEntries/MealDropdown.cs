using UnityEngine;
using TMPro;
using System.Collections.Generic;
using static CalorieCounter.MealEntries.AbstractMeals;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class MealDropdown : MonoBehaviour {

        [SerializeField]
        private MealTypes _mealType = default;

        public Meal SelectedMeal { get; private set; }
        
        private TMP_Dropdown _mealDropdown;
        private List<Meal> _meals;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void RefreshSelectedMeal(int index) {
            if (_meals.Exists(x => x.Name == _optionDataList[index].text)) {
                SelectedMeal = _meals.Find(x => x.Name == _optionDataList[index].text);
            } else {
                SelectedMeal = default;
            }
        }

        public void ResetDropdown() {
            _mealDropdown.value = 0;
            SelectedMeal = default;
        }

        private void Start() {
            _mealDropdown = GetComponent<TMP_Dropdown>();

            if (_mealType == MealTypes.Small) {
                _meals = new List<Meal>(new SmallMeals().Meals);
            } else {
                _meals = new List<Meal>(new LargeMeals().Meals);
            }

            _mealDropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(""));
            foreach (var meal in _meals) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(meal.Name));
            }
            _mealDropdown.AddOptions(_optionDataList);
            SelectedMeal = default;
        }
        
    }
}
