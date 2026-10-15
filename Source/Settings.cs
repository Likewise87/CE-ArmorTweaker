using Verse;

namespace TSA.CE.QualityScaling
{
    public class QualityScaling_Settings : ModSettings
    {
        public float factorAwful = 0.60f;
        public float factorPoor = 0.80f;
        public float factorNormal = 1.00f;
        public float factorGood = 1.15f;
        public float factorExcellent = 1.30f;
        public float factorMasterwork = 1.50f;
        public float factorLegendary = 1.75f;

        public static QualityScaling_Settings Current;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref factorAwful, "factorAwful", 0.60f);
            Scribe_Values.Look(ref factorPoor, "factorPoor", 0.80f);
            Scribe_Values.Look(ref factorNormal, "factorNormal", 1.00f);
            Scribe_Values.Look(ref factorGood, "factorGood", 1.15f);
            Scribe_Values.Look(ref factorExcellent, "factorExcellent", 1.30f);
            Scribe_Values.Look(ref factorMasterwork, "factorMasterwork", 1.50f);
            Scribe_Values.Look(ref factorLegendary, "factorLegendary", 1.75f);
        }

        public void ResetToDefaults()
        {
            factorAwful = 0.60f; factorPoor = 0.80f; factorNormal = 1.00f;
            factorGood = 1.15f; factorExcellent = 1.30f; factorMasterwork = 1.50f; factorLegendary = 1.75f;
        }
    }
}