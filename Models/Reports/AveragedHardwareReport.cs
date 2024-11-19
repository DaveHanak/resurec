using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Models.Reports
{
    public class AveragedHardwareReport : HardwareReport
    {
        public AveragedHardwareReport(IEnumerable<HardwareReport> hardwareReports)
        {
            CpuUsage = hardwareReports.Average(report => report.CpuUsage);
            CpuTemperature = hardwareReports.Average(report => report.CpuTemperature);
            RamUsage = hardwareReports.Average(report => report.RamUsage);
            GpuUsage = hardwareReports.Average(report => report.GpuUsage);
            GpuTemperature = hardwareReports.Average(report => report.GpuTemperature);
        }

        public AveragedHardwareReport(float? cpuUsage, float? cpuTemperature, float? ramUsage, float? gpuUsage, float? gpuTemperature)
        {
            CpuUsage = cpuUsage;
            CpuTemperature = cpuTemperature;
            RamUsage = ramUsage;
            GpuUsage = gpuUsage;
            GpuTemperature = gpuTemperature;
        }
    }
}
