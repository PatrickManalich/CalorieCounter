using System;

namespace CalorieCounter.ScaleEntries {

    public struct ScaleEntry {

        public DateTime date;
        public float weight;
        public float bodyFat;
        public float bodyWater;
        public float muscleMass;
        public float boneMass;
        public float bmi;

        public ScaleEntry(DateTime date, float weight, float bodyFat, float bodyWater, float muscleMass, float boneMass, float bmi) {
            this.date = date;
            this.weight = weight > 0 ? GlobalMethods.Round(weight) : 0;
            this.bodyFat = bodyFat > 0 ? GlobalMethods.Round(bodyFat) : 0;
            this.bodyWater = bodyWater > 0 ? GlobalMethods.Round(bodyWater) : 0;
            this.muscleMass = muscleMass > 0 ? GlobalMethods.Round(muscleMass) : 0;
            this.boneMass = boneMass > 0 ? GlobalMethods.Round(boneMass) : 0;
            this.bmi = bmi > 0 ? GlobalMethods.Round(bmi) : 0;
        }

        public override string ToString() {
            return string.Format("Date: {0}, Weight: {1}, Body Fat: {2}, Body Water: {3}, Muscle Mass: {4}, Bone Mass: {5}, BMI: {5}",
                date.ToShortDateString(), weight, bodyFat, bodyWater, muscleMass, boneMass, bmi);
        }
    }
}
