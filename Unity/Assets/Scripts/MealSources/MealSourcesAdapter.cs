using CalorieCounter;
using CalorieCounter.Managers;
using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

public class MealSourcesAdapter : MonoBehaviour
{
    [System.Serializable]
    private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealSourcesScrollView> { }

    [SerializeField]
    private ScrollViewDictionary _scrollViewDictionary = default;

    public void ExportMealSourcesDictionary() {
        var mealSourcesDictionary = new Dictionary<MealSourceType, SortedList<string, MealSource>>() {
            { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].MealSources },
            { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].MealSources },
        };
        GameManager.MealSourcesManager.ExportMealSourcesDictionary(mealSourcesDictionary);
    }

    private void Start() {
        var importedMealSourcesDictionary = GameManager.MealSourcesManager.ImportMealSourcesDictionary();
        if (importedMealSourcesDictionary != default) {
            foreach (var key in importedMealSourcesDictionary.Keys) {
                var mealSourcesScrollView = _scrollViewDictionary[key];
                var importedMealSources = importedMealSourcesDictionary[key];
                foreach (var mealSource in importedMealSources.Values) {
                    mealSourcesScrollView.AddMealSource(mealSource);
                }
            }
        }
    }
}
