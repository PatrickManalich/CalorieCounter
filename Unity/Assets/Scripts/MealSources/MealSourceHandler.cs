using CalorieCounter;
using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

public class MealSourceHandler : MonoBehaviour
{
    [System.Serializable]
    private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealSourcesScrollView> { }

    [SerializeField]
    private ScrollViewDictionary _scrollViewDict = default;

    private Dictionary<MealSourceType, List<MealSource>> _mealSourcesDict = new Dictionary<MealSourceType, List<MealSource>>() {
            { MealSourceType.Small, new List<MealSource>() },
            { MealSourceType.Large, new List<MealSource>() },
        };

    public void AddMealSource(MealSource mealSource) {
        _mealSourcesDict[mealSource.MealSourceType].Add(mealSource);
    }

    public void ExportMealSourcesDict() {
        JsonConverter.Export(_mealSourcesDict, GlobalPaths.MealSourcesFilePath);
    }

    private void Start() {
        var importedMealSourcesDict = JsonConverter.Import<Dictionary<MealSourceType, List<MealSource>>>(GlobalPaths.MealSourcesFilePath);
        if (importedMealSourcesDict != default) {
            foreach (var key in importedMealSourcesDict.Keys) {
                MealSourcesScrollView scrollView = _scrollViewDict[key];
                foreach (var mealSource in importedMealSourcesDict[key]) {
                    AddMealSource(mealSource);
                    scrollView.AddMealSource(mealSource);
                }
            }
        }
    }
}
