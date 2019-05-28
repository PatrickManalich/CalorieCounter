﻿using CalorieCounter.Globals;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMeals {

        public abstract MealTypes MealType { get; }

        public abstract IReadOnlyList<MealSource> MealSources { get; }

    }
}
