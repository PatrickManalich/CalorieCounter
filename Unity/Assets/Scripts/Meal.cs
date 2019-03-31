namespace CalorieCounter {

    public struct Meal {
        public string Name;
        public string ServingSize;
        public float Fat;
        public float Carbs;
        public float Protein;
        public float Calories;
        public string Description;

        public static readonly Meal NullMeal = new Meal("NullMeal", "", 0, 0, 0, ""); 

        public Meal(string name, string servingSize, float fat, float carbs, float protein, string description) {
            Name = name;
            ServingSize = servingSize;
            Fat = fat > 0 ? fat : 0;
            Carbs = carbs > 0 ? carbs : 0;
            Protein = protein > 0 ? protein : 0;
            Calories = (fat * 9) + (carbs * 4) + (protein * 4);
            Description = description;
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
            return Name + " (per " + ServingSize + "), [ F:" + Fat + ", C:" + Carbs + ", P:" + Protein + " ], " + Description;
        }
    }
}
