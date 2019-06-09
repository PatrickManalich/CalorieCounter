using CalorieCounter.Globals;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMeals {

        public abstract MealType MealType { get; }

        public abstract IReadOnlyList<MealSource> MealSources { get; }

    }
}
