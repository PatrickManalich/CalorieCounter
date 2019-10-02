using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers
{

    public class MealSourcesManager : MonoBehaviour
    {

        private Dictionary<MealSourceType, Dictionary<string, MealSource>> _mealSourcesDictionary = default;

        private Dictionary<MealSourceType, Dictionary<string, string>> _mealSourceNamesDictionary = default;

        private Dictionary<MealSourceType, SortedList<string, NamedMealSource>> _namedMealSourcesDictionary = default;

        private bool _mealSourcesDictionaryImported = false;
        private bool _mealSourceNamesDictionaryImported = false;
        private bool _namedMealSourcesDictionaryImported = false;

        public Dictionary<MealSourceType, Dictionary<string, MealSource>> ImportMealSourcesDictionary()
        {
            if (!_mealSourcesDictionaryImported)
            {
                _mealSourcesDictionary = JsonConverter.Import<Dictionary<MealSourceType, Dictionary<string, MealSource>>>(GlobalPaths.JsonMealSourcesFileName);
                _mealSourcesDictionaryImported = true;
            }
            return _mealSourcesDictionary;
        }

        public Dictionary<MealSourceType, Dictionary<string, string>> ImportMealSourceNamesDictionary()
        {
            if (!_mealSourceNamesDictionaryImported)
            {
                _mealSourceNamesDictionary = JsonConverter.Import<Dictionary<MealSourceType, Dictionary<string, string>>>(GlobalPaths.JsonMealSourceNamesFileName);
                _mealSourceNamesDictionaryImported = true;
            }
            return _mealSourceNamesDictionary;
        }

        public Dictionary<MealSourceType, SortedList<string, NamedMealSource>> ImportNamedMealSourcesDictionary()
        {
            if (!_namedMealSourcesDictionaryImported)
            {
                ImportMealSourcesDictionary();
                ImportMealSourceNamesDictionary();
                _namedMealSourcesDictionary = new Dictionary<MealSourceType, SortedList<string, NamedMealSource>>();
                foreach (var mealSourceType in _mealSourcesDictionary.Keys)
                {
                    _namedMealSourcesDictionary.Add(mealSourceType, new SortedList<string, NamedMealSource>());
                    var mealSources = _mealSourcesDictionary[mealSourceType];
                    var mealSourceNames = _mealSourceNamesDictionary[mealSourceType];
                    foreach (var mealSource in mealSources.Values)
                    {
                        if (mealSourceNames.ContainsKey(mealSource.id))
                        {
                            var mealSourceName = mealSourceNames[mealSource.id];
                            var namedMealSource = new NamedMealSource(mealSourceName, mealSource);
                            _namedMealSourcesDictionary[mealSourceType].Add(namedMealSource.name, namedMealSource);
                        } else
                        {
                            Debug.LogWarning($"No meal source ID found in MealSourceNames for: {mealSource.id}");
                        }
                    }
                }

                _namedMealSourcesDictionaryImported = true;
            }
            return _namedMealSourcesDictionary;
        }


        public void ExportDictionaries(Dictionary<MealSourceType, Dictionary<string, MealSource>> mealSourcesDictionary, Dictionary<MealSourceType, Dictionary<string, string>> mealSourceNamesDictionary)
        {
            JsonConverter.Export(mealSourcesDictionary, GlobalPaths.JsonMealSourcesFileName);
            JsonConverter.Export(mealSourceNamesDictionary, GlobalPaths.JsonMealSourceNamesFileName);
            _mealSourcesDictionaryImported = false;
            _mealSourceNamesDictionaryImported = false;
            _namedMealSourcesDictionaryImported = false;
            ImportNamedMealSourcesDictionary();
        }
    }
}
