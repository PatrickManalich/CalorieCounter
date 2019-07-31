using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealSources {

    public class MealSourcesScrollView : MonoBehaviour
    {

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        [SerializeField]
        private MealSourceInputFields _mealSourceInputFields = default;

        public SortedList<string, MealSource> MealSources { get; private set; } = new SortedList<string, MealSource>();

        public void AddMealSourceFromInputFields()
        {
            AddMealSource(_mealSourceInputFields.GetMealSourceFromInputFields());
        }

        public void AddMealSource(MealSource mealSource)
        {
            MealSources.Add(mealSource.Name, mealSource);

            GameObject mealNameText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject servingSizeText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject descriptionText = Instantiate(_scrollViewTextPrefab, _content.transform);

            int siblingStartIndex = MealSources.IndexOfKey(mealSource.Name) * _content.constraintCount;
            mealNameText.transform.SetSiblingIndex(siblingStartIndex);
            servingSizeText.transform.SetSiblingIndex(siblingStartIndex + 1);
            fatText.transform.SetSiblingIndex(siblingStartIndex + 2);
            carbText.transform.SetSiblingIndex(siblingStartIndex + 3);
            proteinText.transform.SetSiblingIndex(siblingStartIndex + 4);
            calorieText.transform.SetSiblingIndex(siblingStartIndex + 5);
            descriptionText.transform.SetSiblingIndex(siblingStartIndex + 6);

            mealNameText.GetComponent<TextMeshProUGUI>().text = mealSource.Name;
            servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.ServingSize;
            fatText.GetComponent<TextMeshProUGUI>().text = mealSource.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSource.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.Calories.ToString();
            descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.Description;
        }
    }
}
