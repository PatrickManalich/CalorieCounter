using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using CalorieCounter.Globals;

namespace CalorieCounter.MealEntries {

    public class MealEntryImporter : MonoBehaviour {

        [System.Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealTypes, ScrollView> { }

        [SerializeField]
        private ScrollViewDictionary _scrollViewDict = default;

        [SerializeField]
        private MealEntryTracker _mealEntryTracker = default;

        private void Start() {
            MealEntry importedMealEntry = JsonUtility.Import<MealEntry>(_mealEntryTracker.GetMealEntryPath());
            if (importedMealEntry != default) {
                foreach (var key in importedMealEntry.MealProportionsDict.Keys) {
                    ScrollView scrollView = _scrollViewDict[key];
                    foreach (var mealProportion in importedMealEntry.MealProportionsDict[key]) {
                        scrollView.AddMealProportion(mealProportion);
                    }
                }
            }
        }
    }
}
