using Newtonsoft.Json;
using System;

namespace CalorieCounter.TargetEntries {

    public class TargetEntry {

        public DateTime dateTime;
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

        public TargetEntry() { }

        [JsonConstructor]
        public TargetEntry(DateTime dateTime, float weight) {
            this.dateTime = dateTime;
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

        public static bool operator ==(TargetEntry targetEntry1, TargetEntry targetEntry2)
        {
            if (targetEntry1 is null)
            {
                if (targetEntry2 is null)
                {
                    return true;
                }
                return false;
            }
            return targetEntry1.Equals(targetEntry2);
        }

        public static bool operator !=(TargetEntry targetEntry1, TargetEntry targetEntry2)
        {
            return !(targetEntry1 == targetEntry2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TargetEntry);
        }

        public override int GetHashCode()
        {
            return (dateTime, weight, calorieMaintenanceLevel, restDayFat, restDayCarbs, restDayProtein, restDayCalories, trainingDayFat, trainingDayCarbs, trainingDayProtein, trainingDayCalories).GetHashCode();
        }

        public override string ToString() {
            return $"Date: {dateTime.ToShortDateString()}, Weight: {weight}, Calorie Maintenance Level:{calorieMaintenanceLevel}, " +
                $"Rest Day: [ Fat: {restDayFat}, Carbs: {restDayCarbs}, Protein: {restDayProtein}, Calories: {restDayCalories} ], " +
                $"Training Day: [ Fat: {trainingDayFat}, Carbs: {trainingDayCarbs}, Protein: {trainingDayProtein}, Calories: {trainingDayCalories} ]";
        }

        public bool Equals(TargetEntry other)
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
            return (dateTime == other.dateTime) && (weight == other.weight) && (calorieMaintenanceLevel == other.calorieMaintenanceLevel) &&
                (restDayFat == other.restDayFat) && (restDayCarbs == other.restDayCarbs) && (restDayProtein == other.restDayProtein) &&
                (restDayCalories == other.restDayCalories) && (trainingDayFat == other.trainingDayFat) && (trainingDayCarbs == other.trainingDayCarbs) &&
                (trainingDayProtein == other.trainingDayProtein) && (trainingDayCalories == other.trainingDayCalories);
        }
    }
}
