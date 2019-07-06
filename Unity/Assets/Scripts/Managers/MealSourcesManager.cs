using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers
{

    public class MealSourcesManager : MonoBehaviour
    {

        private Dictionary<MealSourceType, SortedList<string, MealSource>> _mealSourcesDictionary;

        private bool _imported = false;

        public Dictionary<MealSourceType, SortedList<string, MealSource>> ImportMealSourcesDictionary()
        {
            if (!_imported)
            {
                _mealSourcesDictionary = JsonConverter.Import<Dictionary<MealSourceType, SortedList<string, MealSource>>>(GlobalPaths.MealSourcesFilePath);
                _imported = true;
            }
            return _mealSourcesDictionary;
        }

        public void ExportMealSourcesDictionary(Dictionary<MealSourceType, SortedList<string, MealSource>> mealSourcesDictionary)
        {
            JsonConverter.Export(mealSourcesDictionary, GlobalPaths.MealSourcesFilePath);
        }
    }
}
