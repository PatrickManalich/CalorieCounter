using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter {

    public class MealScrollView : MonoBehaviour {

        [SerializeField]
        private GameObject _scrollViewTextPrefab;

        private Transform _contentTransform;

        public void AddMeal(float amount, Meal meal) {
            GameObject amountText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject mealText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _contentTransform);

            Meal mealProportion = Meal.GetMealProportion(amount, meal);

            amountText.GetComponent<TextMeshProUGUI>().text = amount.ToString();
            mealText.GetComponent<TextMeshProUGUI>().text = mealProportion.Name;
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.Calories.ToString();
        }

        private void Start() {
            _contentTransform = GetComponentInChildren<GridLayoutGroup>().transform;
        }
    }
}
