using Newtonsoft.Json;
using System;

namespace CalorieCounter.TargetEntries {

    public class TargetEntry {

        public DateTime DateTime { get; }
        public float Weight { get; }
        public float CalorieMaintenanceLevel { get; }
        public float RestDayFat { get; }
        public float RestDayCarbs { get; }
        public float RestDayProtein { get; }
        public float RestDayCalories { get; }
        public float TrainingDayFat { get; }
        public float TrainingDayCarbs { get; }
        public float TrainingDayProtein { get; }
        public float TrainingDayCalories { get; }

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
            DateTime = dateTime;
            Weight = weight;
            CalorieMaintenanceLevel = GlobalMethods.Round(((4.536f * Weight) + (15.88f * HeightInInches) - (5 * AgeInYears) + 5) * ActivityMultiplier);

            RestDayCalories = GlobalMethods.Round(CalorieMaintenanceLevel + RestDayCalorieSurplus);
            RestDayProtein = GlobalMethods.Round(Weight * ProteinPerPound);
            RestDayFat = GlobalMethods.Round(RestDayCalories * RestDayFatPercentage / 9);
            RestDayCarbs = GlobalMethods.Round((RestDayCalories - (RestDayProtein * 4) - RestDayFat * 9) / 4);

            TrainingDayCalories = GlobalMethods.Round(CalorieMaintenanceLevel + TrainingDayCalorieSurplus);
            TrainingDayProtein = GlobalMethods.Round(Weight * ProteinPerPound);
            TrainingDayFat = GlobalMethods.Round(TrainingDayCalories * TrainingDayFatPercentage / 9);
            TrainingDayCarbs = GlobalMethods.Round((TrainingDayCalories - (TrainingDayProtein * 4) - TrainingDayFat * 9) / 4);
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
            return (DateTime, Weight, CalorieMaintenanceLevel, RestDayFat, RestDayCarbs, RestDayProtein, RestDayCalories, TrainingDayFat, TrainingDayCarbs, TrainingDayProtein, TrainingDayCalories).GetHashCode();
        }

        public override string ToString() {
            return $"Date: {DateTime.ToShortDateString()}, Weight: {Weight}, Calorie Maintenance Level:{CalorieMaintenanceLevel}, " +
                $"Rest Day: [ Fat: {RestDayFat}, Carbs: {RestDayCarbs}, Protein: {RestDayProtein}, Calories: {RestDayCalories} ], " +
                $"Training Day: [ Fat: {TrainingDayFat}, Carbs: {TrainingDayCarbs}, Protein: {TrainingDayProtein}, Calories: {TrainingDayCalories} ]";
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
            return (DateTime == other.DateTime) && (Weight == other.Weight) && (CalorieMaintenanceLevel == other.CalorieMaintenanceLevel) &&
                (RestDayFat == other.RestDayFat) && (RestDayCarbs == other.RestDayCarbs) && (RestDayProtein == other.RestDayProtein) &&
                (RestDayCalories == other.RestDayCalories) && (TrainingDayFat == other.TrainingDayFat) && (TrainingDayCarbs == other.TrainingDayCarbs) &&
                (TrainingDayProtein == other.TrainingDayProtein) && (TrainingDayCalories == other.TrainingDayCalories);
        }
    }
}
