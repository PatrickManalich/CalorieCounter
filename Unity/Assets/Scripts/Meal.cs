namespace CalorieCounter {

    public struct Meal {
        public string Name;
        public string ServingSize;
        public float Fat;
        public float Carbs;
        public float Protein;
        public float Calories;
        public string Description;

        public static readonly Meal NullMeal = new Meal(0, 0, 0);

        public Meal(float fat, float carbs, float protein) {
            Name = "";
            ServingSize = "";
            Fat = fat > 0 ? fat : 0;
            Carbs = carbs > 0 ? carbs : 0;
            Protein = protein > 0 ? protein : 0;
            Calories = (fat * 9) + (carbs * 4) + (protein * 4);
            Description = "";
        }

        public Meal(string name, string servingSize, float fat, float carbs, float protein, string description) {
            Name = name;
            ServingSize = servingSize;
            Fat = fat > 0 ? fat : 0;
            Carbs = carbs > 0 ? carbs : 0;
            Protein = protein > 0 ? protein : 0;
            Calories = (fat * 9) + (carbs * 4) + (protein * 4);
            Description = description;
        }

        public static Meal operator +(Meal meal1, Meal meal2) {
            return new Meal(meal1.Fat + meal2.Fat, meal1.Carbs + meal2.Carbs, meal1.Protein + meal2.Protein);
        }

        public static Meal operator -(Meal meal1, Meal meal2) {
            return new Meal(meal1.Fat - meal2.Fat, meal1.Carbs - meal2.Carbs, meal1.Protein - meal2.Protein);
        }

        public static Meal GetMealProportion(float serving, Meal meal) {
            return new Meal(meal.Name, meal.ServingSize, meal.Fat * serving, meal.Carbs * serving, meal.Protein * serving, meal.Description);
        }

        public static bool operator ==(Meal meal1, Meal meal2) {
            return meal1.Equals(meal2);
        }

        public static bool operator !=(Meal meal1, Meal meal2) {
            return !meal1.Equals(meal2);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return Name + " (per " + ServingSize + "), [ Fat:" + Fat + ", Carbs:" + Carbs + ", Protein:" + Protein + ", Calories:" + Calories + " ], " + Description;
        }
    }
}
