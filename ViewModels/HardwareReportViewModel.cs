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
        private readonly HardwareReport _hardwareReport;

        public string CpuUsage => $"{_hardwareReport.CpuUsage:F1}%";
        public string CpuTemperature => $"{_hardwareReport.CpuTemperature:F1}°C";

        public string RamUsage => $"{_hardwareReport.RamUsage:F1}%";

        public string GpuUsage => $"{_hardwareReport.GpuUsage:F1}%";
        public string GpuTemperature => $"{_hardwareReport.GpuTemperature:F1}°C";

        public HardwareReportViewModel(HardwareReport hardwareReport)
        {
            _hardwareReport = hardwareReport;
        }
    }
}
