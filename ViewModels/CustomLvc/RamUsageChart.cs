using LiveChartsCore;
using LiveChartsCore.Geo;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ViewModels.CustomLvc
{
    public class RamUsageChart
    {
        public ISeries[] Series { get; set; }

        public RamUsageChart(
            ObservableCollection<float> ramUsed
        )
        {
            Series = [ new LineSeries<float>
            {
                Name = "Used",
                Values = ramUsed,
                Stroke = new SolidColorPaint(new SkiaSharp.SKColor(0, 0, 88, 0), 1),
                GeometryStroke = null,
                Fill = new SolidColorPaint(new SkiaSharp.SKColor(0, 0, 88, 0), 1),
                GeometryFill = null,
                LineSmoothness = 0,
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            }];
        }
    }
}
