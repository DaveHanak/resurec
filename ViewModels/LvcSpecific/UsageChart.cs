using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Models.Reports;
using resurec.ViewModels.CustomLvc.LvcExtensions;
using LiveChartsCore.Measure;
using SkiaSharp;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using resurec.ViewModels.LvcSpecific;

namespace resurec.ViewModels.CustomLvc
{
    public class UsageChart : IUsageDisplay
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

        private readonly ObservableCollection<float> _usageReads = new(new float[61]);
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; } 
        public Axis[] YAxes { get; set; }

        public DrawMarginFrame Frame { get; set; } =
        new()
        {
            Fill = new SolidColorPaint(gray_dark),
        };

        public UsageChart(string name, AxisPosition position, SKColor strokeColor, SKColor fillColor)
        {
            Series =
            [ new LineSeries<float>
                {
                    Values = _usageReads,
                    Stroke = new SolidColorPaint(strokeColor, 2),
                    GeometryStroke = null,
                    Fill = new SolidColorPaint(fillColor, 2),
                    GeometryFill = null,
                    LineSmoothness = 0,
                    AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                    EasingFunction = null,
                    XToolTipLabelFormatter = null,
                    YToolTipLabelFormatter = null,
                }
            ];

            XAxes =
            [ new()
                {
                    MinLimit = 0,
                    MaxLimit = 60,
                    TextSize = 10,
                    Labeler = (value) => (60 - value).ToString(),
                    CustomSeparators = Enumerable.Range(0, 60/5 + 1).Select(i => (double) i * 5).ToArray(),
                    LabelsPaint = new SolidColorPaint(white),
                    SeparatorsPaint = new SolidColorPaint
                    {
                        Color = gray_lighter,
                        StrokeThickness = 2,
                    },
                    SubseparatorsCount = 4,
                    TicksPaint = new SolidColorPaint
                    {
                        Color = gray_lightest,
                        StrokeThickness = 2,
                    },
                    SubticksPaint = new SolidColorPaint
                    {
                        Color = gray_lightest,
                        StrokeThickness = 2,
                    },
                    AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                    EasingFunction = null,
                }
            ];

            YAxes =
            [ new()
                {
                    Position = position,
                    Name = name,
                    NameTextSize = 40,
                    NamePaint = new SolidColorPaint(white),
                    MinLimit = 0,
                    MaxLimit = 100,
                    TextSize = 10,
                    CustomSeparators = Enumerable.Range(0, 100/10 + 1).Select(i => (double) i * 10).ToArray(),
                    LabelsPaint = new SolidColorPaint(white),
                    SeparatorsPaint = new SolidColorPaint
                    {
                        Color = gray_lighter,
                        StrokeThickness = 2,
                    },
                    SubseparatorsCount = 4,
                    TicksPaint = new SolidColorPaint
                    {
                        Color = gray_lightest,
                        StrokeThickness = 2,
                    },
                    SubticksPaint = new SolidColorPaint
                    {
                        Color = gray_lightest,
                        StrokeThickness = 2,
                    },
                    AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                    EasingFunction = null,
                }
            ];
        }

        public void Update(float value)
        {
            _usageReads.Update60s(value);
        }

        public void RescaleLabels(double actualWidth, double actualHeight)
        {
            var x = XAxes[0];
            var y = YAxes[0];

            var textSize = Math.Sqrt(actualWidth * actualHeight) / 120;
            var nameTextSize = Math.Sqrt(actualWidth * actualHeight) / 30;

            x.TextSize = textSize;
            y.TextSize = textSize;
            y.NameTextSize = nameTextSize;
        }
    }
}
