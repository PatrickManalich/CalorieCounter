using CalorieCounter.Managers;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealSources {

    public class MealSourcesAdapter : MonoBehaviour {

        [SerializeField]
        private Scene _scene = default;

        [System.Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealSourcesScrollView> { }

        [DisplayBasedOnEnum("_scene", (int)Scene.MealSources)]
        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        public List<MealSource> GetMealSources(MealSourceType mealSourceType)
        {
            var importedMealSourcesDictionary = GameManager.MealSourcesManager.ImportMealSourcesDictionary();
            var mealSources = new List<MealSource>();
            foreach (var pair in importedMealSourcesDictionary[mealSourceType])
            {
                mealSources.Add(pair.Value);
            }
            return mealSources;
        }

        public void ExportMealSourcesDictionary()
        {
            var mealSourcesDictionary = new Dictionary<MealSourceType, SortedList<string, MealSource>>() {
                { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].ArchivedMealSources },
                { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].ArchivedMealSources },
            };
            GameManager.MealSourcesManager.ExportMealSourcesDictionary(mealSourcesDictionary);
        }

        private void Start()
        {
            if (_scene != Scene.MealSources)
            {
                return;
            }

            var importedMealSourcesDictionary = GameManager.MealSourcesManager.ImportMealSourcesDictionary();
            if (importedMealSourcesDictionary != default)
            {
                foreach (var key in importedMealSourcesDictionary.Keys)
                {
                    var mealSourcesScrollView = _scrollViewDictionary[key];
                    var importedMealSources = importedMealSourcesDictionary[key];
                    foreach (var mealSource in importedMealSources.Values)
                    {
                        mealSourcesScrollView.AddMealSource(mealSource);
                    }
                }
            }
        }
    }
}
