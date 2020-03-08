using CalorieCounter.Managers;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CalorieCounter.MealSources {

    public class MealSourcesAdapter : AbstractAdapter {

        [SerializeField]
        private Scene _scene = default;

        [Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealSourcesScrollView> { }

        [DisplayBasedOnEnum("_scene", (int)Scene.MealSources)]
        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        private const int MealSourcesCountStartIndex = 0;

        public override void Export()
        {
            var mealSourcesDictionary = new Dictionary<MealSourceType, Dictionary<string, MealSource>>() {
                { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].MealSources },
                { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].MealSources },
            };
            var mealSourceNamesDictionary = new Dictionary<MealSourceType, Dictionary<string, string>>() {
                { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].MealSourceNames },
                { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].MealSourceNames },
            };
            GameManager.MealSourcesManager.ExportDictionaries(mealSourcesDictionary, mealSourceNamesDictionary);
        }

        public static string GetMealSourceName(MealSource mealSource)
        {
            var mealSourceNamesDictionary = GameManager.MealSourcesManager.ImportMealSourceNamesDictionary();
            var mealSourceNames = mealSourceNamesDictionary[mealSource.mealSourceType];
            return mealSourceNames[mealSource.id];
        }

        public static List<NamedMealSource> GetNamedMealSources(MealSourceType mealSourceType)
        {
            var namedMealSourcesDictionary = GameManager.MealSourcesManager.ImportNamedMealSourcesDictionary();
            var namedMealSources = new List<NamedMealSource>();
            if(namedMealSourcesDictionary.Count > 0)
            {
                namedMealSources = namedMealSourcesDictionary[mealSourceType].Values.ToList();
            }
            return namedMealSources;
        }

        private void Start()
        {
            if (_scene != Scene.MealSources)
            {
                return;
            }

            var namedMealSourcesDictionary = GameManager.MealSourcesManager.ImportNamedMealSourcesDictionary();
            if (namedMealSourcesDictionary != default)
            {
                foreach (var mealSourceType in namedMealSourcesDictionary.Keys)
                {
                    var mealSourcesScrollView = _scrollViewDictionary[mealSourceType];
                    var namedMealSources = namedMealSourcesDictionary[mealSourceType];
                    foreach (var namedMealSource in namedMealSources.Values)
                    {
                        mealSourcesScrollView.AddNamedMealSource(namedMealSource);
                    }
                    mealSourcesScrollView.ScrollToTop();
                }
            }
        }
    }
}
