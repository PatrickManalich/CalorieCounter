using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMealsScrollView : MonoBehaviour {

        public abstract List<MealProportion> GetMealProportions();
        public abstract void AddMealProportion(MealProportion mealProportion);
        public abstract void SubtractMealProportion(MealProportion mealProportion);
        public abstract void ClearMealProportions();
    }
}
