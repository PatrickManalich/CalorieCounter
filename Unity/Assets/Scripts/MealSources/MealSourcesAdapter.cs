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

        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        private const int MealSourcesCountStartIndex = 0;

        public override void Export()
        {
            GameManager.MealSourcesManager.ExportDictionaries(GetScrollViewsMealSourcesDictionary(), GetScrollViewsMealSourceNamesDictionary());
        }

        public override bool DoDifferencesExist()
        {
            var importedMealSourcesDictionary = GameManager.MealSourcesManager.ImportMealSourcesDictionary();
            var scrollViewsMealSourcesDictionary = GetScrollViewsMealSourcesDictionary();
            var importedMealSourceNamesDictionary = GameManager.MealSourcesManager.ImportMealSourceNamesDictionary();
            var scrollViewsMealSourceNamesDictionary = GetScrollViewsMealSourceNamesDictionary();

            var doMealSourcesDictionariesDiffer = DoDifferencesExistForNestedDictionary(importedMealSourcesDictionary, scrollViewsMealSourcesDictionary);
            var doMealSourceNamesDictionariesDiffer = DoDifferencesExistForNestedDictionary(importedMealSourceNamesDictionary, scrollViewsMealSourceNamesDictionary);
            return doMealSourcesDictionariesDiffer || doMealSourceNamesDictionariesDiffer;
        }

        public static bool DoesMealSourceExist(MealSource mealSource)
        {
            var mealSourcesDictionary = GameManager.MealSourcesManager.ImportMealSourcesDictionary();
            if (mealSourcesDictionary.Count > 0 && mealSourcesDictionary.ContainsKey(mealSource.mealSourceType))
            {
                var mealSources = mealSourcesDictionary[mealSource.mealSourceType];
                return mealSources.ContainsKey(mealSource.id);
            }
            else
            {
                return false;
            }
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
            foreach (var mealSourceType in namedMealSourcesDictionary.Keys)
            {
                var mealSourcesScrollView = _scrollViewDictionary[mealSourceType];
                var namedMealSources = namedMealSourcesDictionary[mealSourceType];
                foreach (var namedMealSource in namedMealSources.Values)
                {
                    mealSourcesScrollView.AddNamedMealSource(namedMealSource);
                }
                mealSourcesScrollView.ScrollViewAssistant.ScrollToTop();
            }
        }

        private Dictionary<MealSourceType, Dictionary<string, MealSource>> GetScrollViewsMealSourcesDictionary()
        {
            return new Dictionary<MealSourceType, Dictionary<string, MealSource>>() {
                { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].MealSources },
                { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].MealSources },
            };
        }

        private Dictionary<MealSourceType, Dictionary<string, string>> GetScrollViewsMealSourceNamesDictionary()
        {
            return new Dictionary<MealSourceType, Dictionary<string, string>>() {
                { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].MealSourceNames },
                { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].MealSourceNames },
            };
        }

        private static bool DoDifferencesExistForNestedDictionary<T>(Dictionary<MealSourceType, Dictionary<string, T>> dictionary1, Dictionary<MealSourceType, Dictionary<string, T>> dictionary2)
        {
            var doDictionariesDiffer = false;
            if (dictionary1.Keys.Count == dictionary2.Keys.Count &&
                dictionary1.Keys.All(dictionary2.Keys.Contains))
            {
                // Keys are equal
                foreach (var mealSourceType in dictionary1.Keys)
                {
                    var nestedDictionary1 = dictionary1[mealSourceType];
                    var nestedDictionary2 = dictionary2[mealSourceType];

                    if (nestedDictionary1.Keys.Count == nestedDictionary2.Keys.Count &&
                        nestedDictionary1.Keys.All(nestedDictionary2.Keys.Contains))
                    {
                        // Keys are equal
                        if (!nestedDictionary1.Values.All(nestedDictionary2.Values.Contains))
                        {
                            // Values aren't equal
                            doDictionariesDiffer = true;
                            break;
                        }
                    }
                    else
                    {
                        doDictionariesDiffer = true;
                    }
                }
            }
            else
            {
                doDictionariesDiffer = true;
            }
            return doDictionariesDiffer;
        }
    }
}
