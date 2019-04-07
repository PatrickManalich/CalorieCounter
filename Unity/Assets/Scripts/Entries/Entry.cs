using System;

namespace CalorieCounter.Entries {

    public struct Entry {

        public DateTime Date;
        public float Weight;
        public float BodyFat;
        public float BodyWater;
        public float MuscleMass;
        public float BoneMass;
        public float Bmi;

        public Entry(float weight, float bodyFat, float bodyWater, float muscleMass, float boneMass, float bmi) {
            Date = DateTime.Today;
            Weight = weight > 0 ? Round(weight) : 0;
            BodyFat = bodyFat > 0 ? Round(bodyFat) : 0;
            BodyWater = bodyWater > 0 ? Round(bodyWater) : 0;
            MuscleMass = muscleMass > 0 ? Round(muscleMass) : 0;
            BoneMass = boneMass > 0 ? Round(boneMass) : 0;
            Bmi = bmi > 0 ? Round(bmi) : 0;
        }

        public override string ToString() {
            return Date + ", [ Weight:" + Weight + ", BodyFat:" + BodyFat + ", BodyWater:" + BodyWater + ", MuscleMass:" + MuscleMass + ", BoneMass:" + BoneMass + ", Bmi:" + Bmi + " ]";
        }

        private static float Round(float number) {
            return (float)Math.Round(number, 1);
        }
    }
}
