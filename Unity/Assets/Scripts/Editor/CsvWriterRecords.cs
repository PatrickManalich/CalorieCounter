
using CalorieCounter.ScaleEntries;
using CalorieCounter.TargetEntries;

namespace CalorieCounter.EditorExtensions
{

    public class ScaleEntryRecord
    {
        public string Date { get; set; }
        public float Weight { get; set; }
        public float BodyFat { get; set; }
        public float BodyWater { get; set; }
        public float MuscleMass { get; set; }
        public float BoneMass { get; set; }
        public float Bmi { get; set; }

        public ScaleEntryRecord(ScaleEntry scaleEntry)
        {
            Date = scaleEntry.date.ToShortDateString();
            Weight = scaleEntry.weight;
            BodyFat = scaleEntry.bodyFat;
            BodyWater = scaleEntry.bodyWater;
            MuscleMass = scaleEntry.muscleMass;
            BoneMass = scaleEntry.boneMass;
            Bmi = scaleEntry.bmi;
        }
    }


    public class TargetEntryRecord
    {
        public string Date { get; set; }
        public float Weight { get; set; }
        public float CalorieMaintenanceLevel { get; set; }
        public float RestDayFat { get; set; }
        public float RestDayCarbs { get; set; }
        public float RestDayProtein { get; set; }
        public float RestDayCalories { get; set; }
        public float TrainingDayFat { get; set; }
        public float TrainingDayCarbs { get; set; }
        public float TrainingDayProtein { get; set; }
        public float TrainingDayCalories { get; set; }

        public TargetEntryRecord(TargetEntry targetEntry)
        {
            Date = targetEntry.date.ToShortDateString();
            Weight = targetEntry.weight;
            CalorieMaintenanceLevel = targetEntry.calorieMaintenanceLevel;
            RestDayFat = targetEntry.restDayFat;
            RestDayCarbs = targetEntry.restDayCarbs;
            RestDayProtein = targetEntry.restDayProtein;
            RestDayCalories = targetEntry.restDayCalories;
            TrainingDayFat = targetEntry.trainingDayFat;
            TrainingDayCarbs = targetEntry.trainingDayCarbs;
            TrainingDayProtein = targetEntry.trainingDayProtein;
            TrainingDayCalories = targetEntry.trainingDayCalories;
        }
    }
}