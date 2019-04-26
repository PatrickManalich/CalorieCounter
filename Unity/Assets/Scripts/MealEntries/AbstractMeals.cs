using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMeals {

        public enum MealTypes { None, Small, Large }

        public abstract MealTypes MealType { get; }

        public abstract IReadOnlyList<Meal> Meals { get; }

    }
}
