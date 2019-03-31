using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace CalorieCounter {

    public class MealDropdown : MonoBehaviour {

        [SerializeField]
        private AbstractMeals.MealType _mealType;

        private const string ChooseText = "Choose";

        private TMP_Dropdown _mealDropdown;
        private IReadOnlyList<Meal> _meals;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();
        private string _selectedMealName;

        public void DropdownIndexChanged(int index) {
            _selectedMealName = _optionDataList[index].text;
        }

        private void Start() {
            _mealDropdown = GetComponent<TMP_Dropdown>();

            if (_mealType == AbstractMeals.MealType.Small) {
                _meals = new SmallMeals().Meals;
            } else {
                _meals = new LargeMeals().Meals;
            }

            _mealDropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(ChooseText));
            foreach (var meal in _meals) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(meal.Name));
            }
            _mealDropdown.AddOptions(_optionDataList);
            _selectedMealName = _optionDataList[0].text;
        }
        
    }
}
