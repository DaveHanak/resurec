using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Models.Reports;

namespace resurec.DTOs
{
    public class HardwareReportDTO
    {
        public float? CpuUsage { get; set; }
        public float? CpuTemperature { get; set; }
        public float? RamUsage { get; set; }
        public float? GpuUsage { get; set; }
        public float? GpuTemperature { get; set; }

        public HardwareReportDTO() { }
        public HardwareReportDTO(HardwareReport hardwareReport)
        {
            CpuUsage = hardwareReport.CpuUsage;
            CpuTemperature = hardwareReport.CpuTemperature;
            RamUsage = hardwareReport.RamUsage;
            GpuUsage = hardwareReport.GpuUsage;
            GpuTemperature = hardwareReport.GpuTemperature;
        }
    }
}
