using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using resurec.Models.Reports;

namespace resurec.ViewModels.CustomLvc
{
    public class CpuUsageChart : IUsageDisplay
    {
        public ISeries[] Series { get; set; }

        private readonly ObservableCollection<float> _cpuTotal = new(new float[60]);
        private readonly ObservableCollection<float> _cpuCoreMax = new(new float[60]);

        public CpuUsageChart()
        {
            Series = [ new LineSeries<float>
            {
                Name = "Total",
                Values = _cpuTotal,
                Stroke = new SolidColorPaint(new SkiaSharp.SKColor(88, 0, 0, 0), 1),
                GeometryStroke = null,
                Fill = new SolidColorPaint(new SkiaSharp.SKColor(88, 0, 0, 0), 1),
                GeometryFill = null,
                LineSmoothness = 0,
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            }, new LineSeries<float>
            {
                Name = "Core Max",
                Values = _cpuCoreMax,
                Stroke = new SolidColorPaint(new SkiaSharp.SKColor(0, 88, 0, 0), 1),
                GeometryStroke = null,
                Fill = new SolidColorPaint(new SkiaSharp.SKColor(0, 88, 0, 0), 1),
                GeometryFill = null,
                LineSmoothness = 0,
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
            }];
        }

        public void Update(HardwareReport report)
        {
            _cpuTotal.Update60s(report.CpuUsage ?? 0.0f);
            _cpuCoreMax.Update60s(report.GpuUsage ?? 0.0f);
        }
    }
}
