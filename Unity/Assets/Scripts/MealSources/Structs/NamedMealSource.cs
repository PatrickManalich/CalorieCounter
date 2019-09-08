
namespace CalorieCounter.MealSources
{

    public struct NamedMealSource
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
            return namedMealSource1.Equals(namedMealSource2);
        }

        public static bool operator !=(NamedMealSource namedMealSource1, NamedMealSource namedMealSource2)
        {
            return !namedMealSource1.Equals(namedMealSource2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Meal Source: {1}", name, mealSource);
        }
    }
}
