using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Models.Reports
{
    public class HardwareReport
    {
        public double CpuUsage { get; set; }
        public double CpuTemperature { get; set; }

        public double RamUsage { get; set; }

        public double GpuUsage { get; set; }
        public double GpuTemperature { get; set; }
    }
}
