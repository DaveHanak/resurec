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

namespace resurec.ViewModels.CustomLvc
{
    public class GpuUsageChart : IUsageDisplay
    {
        public ISeries[] Series { get; set; }

        public GpuUsageChart(
            ObservableCollection<float> gpuCore
        )
        {
            Series = [ new LineSeries<float>
            {
                Name = "Core",
                Values = gpuCore,
                Stroke = new SolidColorPaint(new SkiaSharp.SKColor(88, 0, 88, 0), 1),
                GeometryStroke = null,
                Fill = new SolidColorPaint(new SkiaSharp.SKColor(88, 0, 88, 0), 1),
                GeometryFill = null,
                LineSmoothness = 0,
                AnimationsSpeed = TimeSpan.FromMilliseconds(0)
            }];
        }

        public void Update(HardwareReport report)
        {
            throw new NotImplementedException();
        }
    }
}
