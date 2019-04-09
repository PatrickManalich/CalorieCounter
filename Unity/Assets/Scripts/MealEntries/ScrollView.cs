using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CalorieCounter.MealEntries {

    public class ScrollView : MonoBehaviour {

        [SerializeField]
        private GameObject _scrollViewTextPrefab;

        [SerializeField]
        private GameObject _deleteButtonContainerPrefab;

        [SerializeField]
        private Transform _contentTransform;

        [System.Serializable]
        public class MealEvent : UnityEvent<Meal> { }

        public MealEvent OnMealSubmitted;

        private List<Meal> _mealProportions = new List<Meal>();

        public void AddMeal(float serving, Meal meal) {
            GameObject amountText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject mealText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _contentTransform);

            Meal mealProportion = Meal.GetMealProportion(serving, meal);

            DeleteButton deleteButton = Instantiate(_deleteButtonContainerPrefab, _contentTransform).GetComponentInChildren<DeleteButton>();
            deleteButton.RemovableGameObjects.InsertRange(0, new List<GameObject> { amountText, mealText, fatText, carbText, proteinText, calorieText });
            deleteButton.MealProportion = mealProportion;

            amountText.GetComponent<TextMeshProUGUI>().text = serving.ToString();
            mealText.GetComponent<TextMeshProUGUI>().text = mealProportion.Name;
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.Calories.ToString();

            _mealProportions.Add(mealProportion);
            OnMealSubmitted.Invoke(mealProportion);
        }
    }
}
