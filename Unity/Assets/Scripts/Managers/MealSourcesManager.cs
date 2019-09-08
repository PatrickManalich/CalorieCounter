using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers
{

    public class MealSourcesManager : MonoBehaviour
    {

        private Dictionary<MealSourceType, SortedList<string, MealSource>> _mealSourcesDictionary = default;

        private Dictionary<MealSourceType, SortedList<string, string>> _mealSourceNamesDictionary = default;

        private Dictionary<MealSourceType, SortedList<string, NamedMealSource>> _namedMealSourcesDictionary = default;

        private bool _mealSourcesDictionaryImported = false;
        private bool _mealSourceNamesDictionaryImported = false;
        private bool _namedMealSourcesDictionaryImported = false;

        public Dictionary<MealSourceType, SortedList<string, MealSource>> ImportMealSourcesDictionary()
        {
            if (!_mealSourcesDictionaryImported)
            {
                _mealSourcesDictionary = JsonConverter.Import<Dictionary<MealSourceType, SortedList<string, MealSource>>>(GlobalPaths.MealSourcesFilePath);
                _mealSourcesDictionaryImported = true;
            }
            return _mealSourcesDictionary;
        }

        public Dictionary<MealSourceType, SortedList<string, string>> ImportMealSourceNamesDictionary()
        {
            if (!_mealSourceNamesDictionaryImported)
            {
                _mealSourceNamesDictionary = JsonConverter.Import<Dictionary<MealSourceType, SortedList<string, string>>>(GlobalPaths.MealSourceNamesFilePath);
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
                    foreach (var mealSource in mealSources.Values)
                    {
                        var mealSourceNames = _mealSourceNamesDictionary[mealSourceType];
                        if (mealSourceNames.ContainsKey(mealSource.Id))
                        {
                            var mealSourceName = mealSourceNames[mealSource.Id];
                            var namedMealSource = new NamedMealSource(mealSourceName, mealSource);
                            _namedMealSourcesDictionary[mealSourceType].Add(namedMealSource.Name, namedMealSource);
                        } else
                        {
                            Debug.LogWarning($"No meal source ID found in MealSourceNames for: {mealSource.Id}");
                        }
                    }
                }

                _namedMealSourcesDictionaryImported = true;
            }
            return _namedMealSourcesDictionary;
        }


        public void ExportDictionaries(Dictionary<MealSourceType, SortedList<string, MealSource>> mealSourcesDictionary, Dictionary<MealSourceType, SortedList<string, string>> mealSourceNamesDictionary)
        {
            JsonConverter.Export(mealSourcesDictionary, GlobalPaths.MealSourcesFilePath);
            JsonConverter.Export(mealSourceNamesDictionary, GlobalPaths.MealSourceNamesFilePath);
            _mealSourcesDictionaryImported = false;
            _mealSourceNamesDictionaryImported = false;
            _namedMealSourcesDictionaryImported = false;
            ImportNamedMealSourcesDictionary();
        }
    }
}
