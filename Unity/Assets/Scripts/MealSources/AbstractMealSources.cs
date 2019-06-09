using CalorieCounter.Globals;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMealSources {

        public abstract MealSourceType MealSourceType { get; }

        public abstract IReadOnlyList<MealSource> MealSources { get; }

    }
}
