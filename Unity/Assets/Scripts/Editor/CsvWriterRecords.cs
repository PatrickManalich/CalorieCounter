
using CalorieCounter.ScaleEntries;

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

}