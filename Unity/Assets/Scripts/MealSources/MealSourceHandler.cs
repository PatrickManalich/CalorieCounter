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

    private Dictionary<MealSourceType, SortedSet<MealSource>> _mealSourcesDict = new Dictionary<MealSourceType, SortedSet<MealSource>>() {
            { MealSourceType.Small, new SortedSet<MealSource>() },
            { MealSourceType.Large, new SortedSet<MealSource>() },
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
                }
                foreach (var mealSource in _mealSourcesDict[key]) {
                    scrollView.AddMealSource(mealSource);
                }
            }
        }
    }
}
