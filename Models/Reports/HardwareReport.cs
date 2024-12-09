using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Models.Reports
{
    public class HardwareReport : IReport
    {
        public float CpuUsage { get; set; }
        public float CpuTemperature { get; set; }

        public float RamUsage { get; set; }

        public float GpuUsage { get; set; }
        public float GpuTemperature { get; set; }

        public HardwareReport() { }

        public HardwareReport(float cpuUsage, float cpuTemperature, float ramUsage, float gpuUsage, float gpuTemperature)
        {
            CpuUsage = cpuUsage;
            CpuTemperature = cpuTemperature;
            RamUsage = ramUsage;
            GpuUsage = gpuUsage;
            GpuTemperature = gpuTemperature;
        }
    }
}
