using CalorieCounter.Managers;
using System;
using UnityEngine;

namespace CalorieCounter.MealSources
{
    [Serializable]
    public class SerializableMealSource
    {
        [SerializeField]
        private string _id = default;
        public string Id => _id;

        private MealSource _mealSource;

        public MealSource GetMealSource()
        {
            if(_mealSource == null)
            {
                var mealSourcesDictionary = GameManager.MealSourcesManager.ImportMealSourcesDictionary();
                foreach (var mealSources in mealSourcesDictionary.Values)
                {
                    if (mealSources.ContainsKey(Id))
                    {
                        _mealSource = mealSources[Id];
                        break;
                    }
                }
                if (_mealSource == null)
                {
                    Debug.LogWarning($"Unable to find meal source for ID: {Id}");
                }
            }
            return _mealSource;
        }
    }
}
