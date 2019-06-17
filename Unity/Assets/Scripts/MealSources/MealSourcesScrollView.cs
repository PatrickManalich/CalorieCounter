using CalorieCounter.MealSources;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MealSourcesScrollView : MonoBehaviour
{

    [SerializeField]
    private MealSourceHandler _mealSourceHandler = default;

    [SerializeField]
    private GameObject _scrollViewTextPrefab = default;

    [SerializeField]
    private GridLayoutGroup _content = default;

    [SerializeField]
    private MealSourceInputFields _mealSourceInputFields = default;

    private SortedList<string, MealSource> _mealSources = new SortedList<string, MealSource>();

    public void AddMealSourceFromInputFields() {
        MealSource mealSource = _mealSourceInputFields.GetMealSourceFromInputFields();
        AddMealSource(mealSource);
        _mealSourceHandler.AddMealSource(mealSource);
    }

    public void AddMealSource(MealSource mealSource) {
        _mealSources.Add(mealSource.Name, mealSource);

        GameObject mealNameText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject servingSizeText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject descriptionText = Instantiate(_scrollViewTextPrefab, _content.transform);

        int siblingStartIndex = _mealSources.IndexOfKey(mealSource.Name) * _content.constraintCount;
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
