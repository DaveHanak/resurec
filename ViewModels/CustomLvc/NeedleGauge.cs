using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ViewModels.CustomLvc
{
    public class NeedleGauge
    {
        private readonly NeedleVisual needle = new();

        public IEnumerable<ISeries> Series { get; set; }
        public IEnumerable<VisualElement<SkiaSharpDrawingContext>> VisualElements { get; set; }

        public NeedleGauge()
        {
            var sectionsOuter = 130;
            var sectionsWidth = 20;

            Series = GaugeGenerator.BuildAngularGaugeSections(
                new GaugeItem(60, s => { s.OuterRadiusOffset = sectionsOuter; s.MaxRadialColumnWidth = sectionsWidth; }),
                new GaugeItem(30, s => { s.OuterRadiusOffset = sectionsOuter; s.MaxRadialColumnWidth = sectionsWidth; }),
                new GaugeItem(10, s => { s.OuterRadiusOffset = sectionsOuter; s.MaxRadialColumnWidth = sectionsWidth; })
            );

            VisualElements =
            [
                new AngularTicksVisual
                {
                    LabelsSize = 16,
                    LabelsOuterOffset = 15,
                    OuterOffset = 65,
                    TicksLength = 20,
                },
                needle
            ];
        }

        public void UpdateNeedle(float? value)
        {
            if (value.HasValue)
            {
                needle.Value = value.Value;
            }
        }
    }
}
