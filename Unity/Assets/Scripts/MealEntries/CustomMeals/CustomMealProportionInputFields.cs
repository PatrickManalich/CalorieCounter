using CalorieCounter.MealSources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CalorieCounter.MealEntries.CustomMeals
{
    public class CustomMealProportionInputFields : InputFields {

        public MealProportion CustomMealProportion {
            get {
                var servingAmount = float.Parse(_inputFields[0].text);
                var fat = float.Parse(_inputFields[1].text);
                var carbs = float.Parse(_inputFields[2].text);
                var protein = float.Parse(_inputFields[3].text);
                var customMealSource = MealSource.CreateCustomMealSource(fat, carbs, protein);
                return new MealProportion(servingAmount, customMealSource);
            }
        }

        [SerializeField]
        private List<GameObject> _blanks = default;

        [SerializeField]
        private MealProportionsScrollView _mealProportionsScrollView = default;

        public override void Show() {
            base.Show();
            var transforms = new List<Transform>();
            for (int i = 0; i < _inputFields.Count + _blanks.Count; i++)
            {
                if (i == 0 || i >= 2 && i <= 4)
                {
                    var inputField = i == 0 ? _inputFields[i] : _inputFields[i - 1];
                    transforms.Add(inputField.transform);
                }
                else
                {
                    var blank = i == 1 ? _blanks[i - 1] : _blanks[i - 4];
                    transforms.Add(blank.transform);
                }
            }
            _mealProportionsScrollView.AddToScrollView(transforms);
            _inputFields.First().ActivateInputField();
        }

        public override void Hide() {
            base.Hide();
            foreach (var inputField in _inputFields) {
                inputField.text = string.Empty;
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform, false);
            }
            foreach (var blank in _blanks) {
                blank.SetActive(false);
                blank.transform.SetParent(transform, false);
            }
        }
    }
}
