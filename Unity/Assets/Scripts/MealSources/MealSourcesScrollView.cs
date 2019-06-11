using CalorieCounter.MealSources;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MealSourcesScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject _scrollViewTextPrefab = default;

    [SerializeField]
    private GridLayoutGroup _content = default;

    [SerializeField]
    private MealSourceInputFields _mealSourceInputFields = default;

    private List<MealSource> _mealSources = new List<MealSource>();

    public void AddMealSourceFromInputFields() {
        MealSource mealSource = _mealSourceInputFields.GetMealSourceFromInputFields();

        GameObject mealNameText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject servingSizeText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);
        GameObject descriptionText = Instantiate(_scrollViewTextPrefab, _content.transform);

        mealNameText.transform.SetSiblingIndex(0);
        servingSizeText.transform.SetSiblingIndex(1);
        fatText.transform.SetSiblingIndex(2);
        carbText.transform.SetSiblingIndex(3);
        proteinText.transform.SetSiblingIndex(4);
        calorieText.transform.SetSiblingIndex(5);
        descriptionText.transform.SetSiblingIndex(6);

        mealNameText.GetComponent<TextMeshProUGUI>().text = mealSource.Name;
        servingSizeText.GetComponent<TextMeshProUGUI>().text = mealSource.ServingSize;
        fatText.GetComponent<TextMeshProUGUI>().text = mealSource.Fat.ToString();
        carbText.GetComponent<TextMeshProUGUI>().text = mealSource.Carbs.ToString();
        proteinText.GetComponent<TextMeshProUGUI>().text = mealSource.Protein.ToString();
        calorieText.GetComponent<TextMeshProUGUI>().text = mealSource.Calories.ToString();
        descriptionText.GetComponent<TextMeshProUGUI>().text = mealSource.Description;
        _mealSources.Add(mealSource);
    }
}
