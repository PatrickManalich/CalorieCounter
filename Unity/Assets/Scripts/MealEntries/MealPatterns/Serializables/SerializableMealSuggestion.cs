using System;
using UnityEngine;

namespace CalorieCounter.MealEntries
{
    [Serializable]
    public class SerializableMealSuggestion
    {
        [SerializeField]
        private SerializableMealProportion _serializableMealProportion = default;
        public SerializableMealProportion SerializableMealProportion => _serializableMealProportion;

        [SerializeField]
        private int _priority = default;
        public int Priority => _priority;

        private MealSuggestion _mealSuggestion;

        public MealSuggestion GetMealSuggestion()
        {
            if (_mealSuggestion == null)
            {
                var mealProportion = SerializableMealProportion.GetMealProportion();
                _mealSuggestion = new MealSuggestion(mealProportion, Priority);
            }
            return _mealSuggestion;
        }
    }
}
