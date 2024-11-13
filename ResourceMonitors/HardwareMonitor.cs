using LibreHardwareMonitor.Hardware;
using resurec.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ResourceMonitors
{
    public class HardwareMonitor : IDisposable
    {
        private readonly Computer _computer;
        private readonly List<HardwareReport> _reports;

        public HardwareMonitor()
        {
            _computer = new Computer()
            {
                IsCpuEnabled = true,
                IsMemoryEnabled = true,
                IsGpuEnabled = true,
            };
            _computer.Open();
        }

        public HardwareReport CreateHardwareReport()
        {
            var report = new HardwareReport();
            using SensorType
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
                switch (hardware.HardwareType)
                {
                    case HardwareType.Cpu:
                        report.CpuUsage = GetSensorValue(hardware, SensorType.Load, )
                }
            }
        }

        private float? GetSensorValue(IHardware hardware, SensorType sensorType, string? sensorName = null)
        {
            var sensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == sensorType && (sensorName == null || s.Name == sensorName));
            return sensor?.Value;
        }

        public void Dispose()
        {
            _computer.Close();
        }
    }
}
