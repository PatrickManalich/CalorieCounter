using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace CalorieCounter {

    public class MealDropdown : MonoBehaviour {

        [SerializeField]
        private AbstractMeals.MealType _mealType;

        public Meal SelectedMeal { get; private set; }

        private const string ChooseText = "Choose";

        private TMP_Dropdown _mealDropdown;
        private List<Meal> _meals;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void DropdownIndexChanged(int index) {
            SelectedMeal = _meals.Find(x => x.Name == _optionDataList[index].text);
        }

        private void Start() {
            _mealDropdown = GetComponent<TMP_Dropdown>();

            if (_mealType == AbstractMeals.MealType.Small) {
                _meals = new List<Meal>(new SmallMeals().Meals);
            } else {
                _meals = new List<Meal>(new LargeMeals().Meals);
            }

            _mealDropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(ChooseText));
            foreach (var meal in _meals) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(meal.Name));
            }
            _mealDropdown.AddOptions(_optionDataList);
            SelectedMeal = Meal.NullMeal;
        }
        
    }
}
