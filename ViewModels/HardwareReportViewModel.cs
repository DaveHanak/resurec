using resurec.Models;
using resurec.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ViewModels
{
    public class HardwareReportViewModel : ViewModelBase, IDisposable
    {
        private HardwareReport? _hardwareReport;

        public string CpuUsage => _hardwareReport?.CpuUsage.HasValue == true ? $"{_hardwareReport.CpuUsage.Value:F1} %" : "N/A";
        public string CpuTemperature => _hardwareReport?.CpuTemperature.HasValue == true ? $"{_hardwareReport.CpuTemperature.Value:F1} °C" : "N/A";

        public string RamUsage => _hardwareReport?.RamUsage.HasValue == true ? $"{_hardwareReport.RamUsage.Value:F1} %" : "N/A";

        public string GpuUsage => _hardwareReport?.GpuUsage.HasValue == true ? $"{_hardwareReport.GpuUsage.Value:F1} %" : "N/A";
        public string GpuTemperature => _hardwareReport?.GpuTemperature.HasValue == true ? $"{_hardwareReport.GpuTemperature.Value:F1} °C" : "N/A";

        public HardwareReportViewModel(HardwareReport hardwareReport)
        {
            _hardwareReport = hardwareReport;
        }
    }
}
