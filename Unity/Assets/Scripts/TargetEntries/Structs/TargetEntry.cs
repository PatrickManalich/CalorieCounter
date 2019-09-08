using System;

namespace CalorieCounter.TargetEntries {

    public struct TargetEntry {

        public DateTime date;
        public float weight;
        public float calorieMaintenanceLevel;
        public float restDayFat;
        public float restDayCarbs;
        public float restDayProtein;
        public float restDayCalories;
        public float trainingDayFat;
        public float trainingDayCarbs;
        public float trainingDayProtein;
        public float trainingDayCalories;

        private const float HeightInInches = 68;
        private const int AgeInYears = 23;
        private const float ActivityMultiplier = 1.375f;
        private const int WeeklyCalorieSurplus = 2100;
        private const int TrainingDays = 3;
        private const int RestDayCalorieSurplus = 150;
        private const int TrainingDayCalorieSurplus = (WeeklyCalorieSurplus - ((7 - TrainingDays) * RestDayCalorieSurplus)) / TrainingDays;
        private const float ProteinPerPound = 1.15f;
        private const float RestDayFatPercentage = 0.28f;
        private const float TrainingDayFatPercentage = 0.22f;

        public TargetEntry(DateTime date, float weight) {
            this.date = date;
            this.weight = weight;
            calorieMaintenanceLevel = GlobalMethods.Round(((4.536f * this.weight) + (15.88f * HeightInInches) - (5 * AgeInYears) + 5) * ActivityMultiplier);

            restDayCalories = GlobalMethods.Round(calorieMaintenanceLevel + RestDayCalorieSurplus);
            restDayProtein = GlobalMethods.Round(this.weight * ProteinPerPound);
            restDayFat = GlobalMethods.Round(restDayCalories * RestDayFatPercentage / 9);
            restDayCarbs = GlobalMethods.Round((restDayCalories - (restDayProtein * 4) - restDayFat * 9) / 4);

            trainingDayCalories = GlobalMethods.Round(calorieMaintenanceLevel + TrainingDayCalorieSurplus);
            trainingDayProtein = GlobalMethods.Round(this.weight * ProteinPerPound);
            trainingDayFat = GlobalMethods.Round(trainingDayCalories * TrainingDayFatPercentage / 9);
            trainingDayCarbs = GlobalMethods.Round((trainingDayCalories - (trainingDayProtein * 4) - trainingDayFat * 9) / 4);
        }

        public static bool operator ==(TargetEntry targetEntry1, TargetEntry targetEntry2) {
            return targetEntry1.Equals(targetEntry2);
        }

        public static bool operator !=(TargetEntry targetEntry1, TargetEntry targetEntry2) {
            return !targetEntry1.Equals(targetEntry2);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return string.Format("Date: {0}, Weight: {1}, Calorie Maintenance Level:{2}, Rest Day: [ Fat: {3}, Carbs: {4}, Protein: {5}, Calories: {6} ], Training Day: [ Fat: {7}, Carbs: {8}, Protein: {9}, Calories: {10} ]",
                date.ToShortDateString(), weight, calorieMaintenanceLevel, restDayFat, restDayCarbs, restDayProtein, restDayCalories, trainingDayFat, trainingDayCarbs, trainingDayProtein, trainingDayCalories);
        }
    }
}
