using CalorieCounter.MealSources;
using System;
using UnityEngine;

namespace CalorieCounter.MealEntries
{
    [Serializable]
    public class SerializableMealProportion
    {
        [SerializeField]
        private float _servingAmount = default;
        public float ServingAmount => _servingAmount;

        [SerializeField]
        private SerializableMealSource _serializableMealSource = default;
        public SerializableMealSource SerializableMealSource => _serializableMealSource;

        private MealProportion _mealProportion;

        public MealProportion GetMealProportion()
        {
            if(_mealProportion == null)
            {
                var mealSource = SerializableMealSource.GetMealSource();
                _mealProportion = new MealProportion(ServingAmount, mealSource);
            }
            return _mealProportion;
        }
    }
}
