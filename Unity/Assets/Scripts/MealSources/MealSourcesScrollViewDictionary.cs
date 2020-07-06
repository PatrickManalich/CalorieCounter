using System;
using RotaryHeart.Lib.SerializableDictionary;

namespace CalorieCounter.MealSources
{
    [Serializable]
    public class MealSourcesScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealSourcesScrollView> { }
}
