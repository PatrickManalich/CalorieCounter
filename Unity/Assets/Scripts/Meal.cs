namespace CalorieCounter {

    public struct Meal {
        public string Name;
        public string Serving;
        public float Fat;
        public float Carbs;
        public float Protein;
        public string Description;

        public Meal(string name, string serving, float fat, float carbs, float protein, string description) {
            Name = name;
            Serving = serving;
            Fat = fat > 0 ? fat : 0;
            Carbs = carbs > 0 ? carbs : 0;
            Protein = protein > 0 ? protein : 0;
            Description = description;
        }

        public override string ToString() {
            return Name + " (per " + Serving + "), [ F:" + Fat + ", C:" + Carbs + ", P:" + Protein + " ], " + Description;
        }
    }
}
