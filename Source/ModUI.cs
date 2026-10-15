using System;
using UnityEngine;
using Verse;

namespace TSA.CE.QualityScaling
{
    public enum SliderFormat { Fixed0, Fixed1, Fixed2, Multiplier, Percent, PercentDecimal }

    public class ModInit : Mod
    {
        public ModInit(ModContentPack content) : base(content)
        {
            QualityScaling_Settings.Current = GetSettings<QualityScaling_Settings>();
        }

        public override string SettingsCategory() => "CE Armor Quality Scaling";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var s = QualityScaling_Settings.Current;
            Listing_Standard l = new Listing_Standard();
            l.Begin(inRect);

            DrawResetButton(l, () => s.ResetToDefaults());

            l.Label("Armor Multipliers (Sharp, Blunt, Heat)");
            l.Gap(6f);

            s.factorAwful = LabeledSlider(l, "Awful", s.factorAwful, 0.1f, 3.0f,
                "Armor rating multiplier at Awful quality.", 0.01f, SliderFormat.Multiplier, 0.60f);
            s.factorPoor = LabeledSlider(l, "Poor", s.factorPoor, 0.1f, 3.0f,
                "Armor rating multiplier at Poor quality.", 0.01f, SliderFormat.Multiplier, 0.80f);
            s.factorNormal = LabeledSlider(l, "Normal", s.factorNormal, 0.1f, 3.0f,
                "Armor rating multiplier at Normal quality.", 0.01f, SliderFormat.Multiplier, 1.00f);
            s.factorGood = LabeledSlider(l, "Good", s.factorGood, 0.1f, 3.0f,
                "Armor rating multiplier at Good quality.", 0.01f, SliderFormat.Multiplier, 1.15f);
            s.factorExcellent = LabeledSlider(l, "Excellent", s.factorExcellent, 0.1f, 3.0f,
                "Armor rating multiplier at Excellent quality.", 0.01f, SliderFormat.Multiplier, 1.30f);
            s.factorMasterwork = LabeledSlider(l, "Masterwork", s.factorMasterwork, 0.1f, 3.0f,
                "Armor rating multiplier at Masterwork quality.", 0.01f, SliderFormat.Multiplier, 1.50f);
            s.factorLegendary = LabeledSlider(l, "Legendary", s.factorLegendary, 0.1f, 3.0f,
                "Armor rating multiplier at Legendary quality.", 0.01f, SliderFormat.Multiplier, 1.75f);

            l.End();
        }

        // ----- WD-style helpers (kept in ModUI; no separate library file) -----

        private static void DrawResetButton(Listing_Standard l, Action onReset)
        {
            if (onReset == null) return;
            l.Gap(4f);
            Rect row = l.GetRect(30f);
            float resetW = Mathf.Min(250f, row.width);
            if (resetW < 120f) resetW = Mathf.Max(100f, row.width * 0.35f);
            Rect resetRect = new Rect(row.x, row.y, resetW, row.height);
            Color prev = GUI.color;
            GUI.color = new Color(1f, 0.5f, 0.5f);
            if (Widgets.ButtonText(resetRect, "Restore defaults")) onReset();
            GUI.color = prev;
            l.Gap(6f);
        }

        private static float LabeledSlider(Listing_Standard l, string label, float val, float min, float max, string tooltip = null, float step = 0.1f, SliderFormat format = SliderFormat.Fixed1, float? defaultValue = null)
        {
            l.Gap(2f);
            Rect r = l.GetRect(24f);
            if (!tooltip.NullOrEmpty()) TooltipHandler.TipRegion(r, TooltipWithDefault(tooltip, defaultValue, format));
            Widgets.Label(r.LeftPart(0.5f), $"{label}: {FormatValue(val, format).Colorize(Color.cyan)}");
            return Widgets.HorizontalSlider(r.RightPart(0.5f), val, min, max, false, null, null, null, step);
        }

        private static string TooltipWithDefault(string tooltip, float? defaultValue, SliderFormat format = SliderFormat.Fixed1)
        {
            if (!defaultValue.HasValue) return tooltip;
            return AppendDefault(tooltip, FormatValue(defaultValue.Value, format));
        }

        private static string AppendDefault(string tooltip, string def)
        {
            string line = $"Default: {def}";
            return tooltip.NullOrEmpty() ? line : tooltip + "\n" + line;
        }

        private static string FormatValue(float val, SliderFormat format) => format switch
        {
            SliderFormat.Fixed0 => val.ToString("F0"),
            SliderFormat.Fixed1 => val.ToString("F1"),
            SliderFormat.Fixed2 => val.ToString("F2"),
            SliderFormat.Multiplier => val.ToString("F2") + "x",
            SliderFormat.Percent => (val * 100f).ToString("F0") + " %",
            SliderFormat.PercentDecimal => (val * 100f).ToString("F1") + " %",
            _ => val.ToString()
        };
    }
}
