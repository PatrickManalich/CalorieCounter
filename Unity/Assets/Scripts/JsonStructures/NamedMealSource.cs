using System;

namespace CalorieCounter.MealSources
{
    [Serializable]
    public class NamedMealSource
    {

        public string name;

        public MealSource mealSource;

        public NamedMealSource(string name, MealSource mealSource)
        {
            this.name = name;
            this.mealSource = mealSource;
        }

        public static bool operator ==(NamedMealSource namedMealSource1, NamedMealSource namedMealSource2)
        {
            if (namedMealSource1 is null)
            {
                if (namedMealSource2 is null)
                {
                    return true;
                }
                return false;
            }
            return namedMealSource1.Equals(namedMealSource2);
        }

        public static bool operator !=(NamedMealSource namedMealSource1, NamedMealSource namedMealSource2)
        {
            return !(namedMealSource1 == namedMealSource2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NamedMealSource);
        }

        public override int GetHashCode()
        {
            return (name, mealSource).GetHashCode();
        }

        public override string ToString()
        {
            return $"Name: {name}, Meal Source: {mealSource}";
        }

        public bool Equals(NamedMealSource other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (GetType() != other.GetType())
            {
                return false;
            }
            return (name == other.name) && (mealSource == other.mealSource);
        }
    }
}
