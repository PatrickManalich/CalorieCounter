using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter {

    public class MealScrollView : MonoBehaviour {

        [SerializeField]
        private GameObject _scrollViewTextPrefab;

        private Transform _contentTransform;

        public void AddMeal(Meal meal) {
            GameObject nameText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _contentTransform);

            nameText.GetComponent<TextMeshProUGUI>().text = meal.Name;
            fatText.GetComponent<TextMeshProUGUI>().text = meal.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = meal.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = meal.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = meal.Calories.ToString();
        }

        private void Start() {
            _contentTransform = GetComponentInChildren<GridLayoutGroup>().transform;
        }
    }
}
