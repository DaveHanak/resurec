using resurec.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ViewModels
{
    public class HardwareReportViewModel : ViewModelBase
    {
        private HardwareReport _hardwareReport;

        public string CpuUsage => _hardwareReport.CpuUsage.HasValue ? $"{_hardwareReport.CpuUsage.Value:F1} %" : "N/A";
        public string CpuTemperature => _hardwareReport.CpuTemperature.HasValue ? $"{_hardwareReport.CpuTemperature.Value:F1} °C" : "N/A";

        public string RamUsage => _hardwareReport.RamUsage.HasValue ? $"{_hardwareReport.RamUsage.Value:F1} %" : "N/A";

        public string GpuUsage => _hardwareReport.GpuUsage.HasValue ? $"{_hardwareReport.GpuUsage.Value:F1} %" : "N/A";
        public string GpuTemperature => _hardwareReport.GpuTemperature.HasValue ? $"{_hardwareReport.GpuTemperature.Value:F1} °C" : "N/A";

        public HardwareReportViewModel(HardwareReport hardwareReport)
        {
            _hardwareReport = hardwareReport;
        }

        public void UpdateReport(HardwareReport hardwareReport)
        {
            _hardwareReport = hardwareReport;
        }
    }
}
