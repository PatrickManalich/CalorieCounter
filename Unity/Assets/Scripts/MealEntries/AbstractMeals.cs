using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMeals {

        public enum MealType { Small, Large }

        public abstract IReadOnlyList<Meal> Meals { get; }

    }
}
