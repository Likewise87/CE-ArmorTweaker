using RimWorld;
using Verse;

namespace TSA.CE.QualityScaling
{
    public class StatPart_QualityCustom : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing && req.Thing.TryGetQuality(out QualityCategory qc))
            {
                float factor = GetFactor(qc);
                // Log.Message($"[TSA] Scaling {parentStat.defName} for {req.Thing.Label} (Quality: {qc}): x{factor}");
                val *= factor;
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (req.HasThing && req.Thing.TryGetQuality(out QualityCategory qc))
            {
                return $"Quality {qc}: x{GetFactor(qc):0.00}";
            }
            return null;
        }

        private float GetFactor(QualityCategory qc)
        {
            var s = QualityScaling_Settings.Current;
            if (s == null) return 1f;

            switch (qc)
            {
                case QualityCategory.Awful: return s.factorAwful;
                case QualityCategory.Poor: return s.factorPoor;
                case QualityCategory.Normal: return s.factorNormal;
                case QualityCategory.Good: return s.factorGood;
                case QualityCategory.Excellent: return s.factorExcellent;
                case QualityCategory.Masterwork: return s.factorMasterwork;
                case QualityCategory.Legendary: return s.factorLegendary;
                default: return 1f;
            }
        }
    }
}