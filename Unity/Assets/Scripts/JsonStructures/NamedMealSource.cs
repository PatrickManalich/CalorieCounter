using Newtonsoft.Json;

namespace CalorieCounter.MealSources
{
    public class NamedMealSource
    {

        public string Name { get; }
        public MealSource MealSource { get; }

        public const string CustomMealSourceName = "Custom Meal";

        public NamedMealSource() { }

        [JsonConstructor]
        public NamedMealSource(string name, MealSource mealSource)
        {
            Name = name;
            MealSource = mealSource;
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
            return (Name, MealSource).GetHashCode();
        }

        public override string ToString()
        {
            return $"Name: {Name}, Meal Source: {MealSource}";
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
            return (Name == other.Name) && (MealSource == other.MealSource);
        }
    }
}
