using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CalorieCounter.MealEntries {

    public class ScrollView : MonoBehaviour {

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private GameObject _deleteButtonContainerPrefab = default;

        [SerializeField]
        private Transform _contentTransform = default;

        [System.Serializable]
        public class MealProportionEvent : UnityEvent<MealProportion> { }

        public MealProportionEvent OnMealProportionSubmitted;

        private List<MealProportion> _mealProportions = new List<MealProportion>();

        public void AddMealProportion(float servingAmount, MealSource mealSource) {
            GameObject servingAmountText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject nameText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _contentTransform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _contentTransform);

            MealProportion mealProportion = new MealProportion(servingAmount, mealSource);

            DeleteButton deleteButton = Instantiate(_deleteButtonContainerPrefab, _contentTransform).GetComponentInChildren<DeleteButton>();
            deleteButton.RemovableGameObjects.InsertRange(0, new List<GameObject> { servingAmountText, nameText, fatText, carbText, proteinText, calorieText });
            deleteButton.MealProportion = mealProportion;

            servingAmountText.GetComponent<TextMeshProUGUI>().text = servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = mealProportion.Source.Name;
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.Calories.ToString();

            _mealProportions.Add(mealProportion);
            OnMealProportionSubmitted.Invoke(mealProportion);
        }
    }
}
