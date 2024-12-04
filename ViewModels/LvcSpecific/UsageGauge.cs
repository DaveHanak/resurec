using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using resurec.Models.Reports;
using SkiaSharp;

namespace resurec.ViewModels.CustomLvc
{
    public class UsageGauge : IUsageDisplay
    {
        //todo move to some global color palette
        private static readonly SKColor white = new(250, 250, 250);
        private static readonly SKColor gray_lightest = new(225, 225, 225);
        private static readonly SKColor gray_lighter = new(200, 200, 200);
        private static readonly SKColor gray_light = new(150, 150, 150);
        private static readonly SKColor gray = new(125, 125, 125);
        private static readonly SKColor gray_dark = new(100, 100, 100);
        private static readonly SKColor gray_darker = new(75, 75, 75);
        private static readonly SKColor gray_darkest = new(50, 50, 50);
        private static readonly SKColor black = new(0, 0, 0);

        public IEnumerable<ISeries> Series { get; set; }
        public ObservableValue GaugeValue { get; set; } = new() { Value = 0 };

        public UsageGauge(string unit, int fatness, SKColor fillColor)
        {
            Series = GaugeGenerator.BuildAngularGaugeSections(
            new GaugeItem(GaugeValue, s =>
            {
                s.Fill = new SolidColorPaint(fillColor);
                s.DataLabelsMaxWidth = 150;
                s.DataLabelsSize = 40;
                s.DataLabelsPaint = new SolidColorPaint(white);
                s.DataLabelsPosition = PolarLabelsPosition.ChartCenter;
                s.DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue}{unit}";
                s.MaxRadialColumnWidth = fatness;
                s.AnimationsSpeed = TimeSpan.FromMilliseconds(0);
                s.EasingFunction = null;
                s.ToolTipLabelFormatter = null;
            }),
            new GaugeItem(GaugeItem.Background, s =>
            {
                s.MaxRadialColumnWidth = fatness;
                s.Fill = new SolidColorPaint(gray_dark);
                s.AnimationsSpeed = TimeSpan.FromMilliseconds(0);
                s.EasingFunction = null;
            }));
        }

        public void Update(float value)
        {
            GaugeValue.Value = (int)value;
        }

        public void RescaleLabels(double actualWidth, double actualHeight)
        {
            var s = (PieSeries<ObservableValue>)Series.First();
            s.DataLabelsSize = Math.Sqrt(actualWidth * actualHeight) / 30;
        }
    }
}
