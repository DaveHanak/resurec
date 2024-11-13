﻿using LibreHardwareMonitor.Hardware;
using resurec.Models.Reports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace resurec.ResourceMonitors
{
    public class HardwareMonitor : IMonitor<HardwareReport>, IDisposable
    {
        private readonly Computer _computer;

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

        public HardwareReport CreateReport()
        {
            var report = new HardwareReport();
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
                switch (hardware.HardwareType)
                {
                    case HardwareType.Cpu:
                        report.CpuUsage = GetSensorValue(hardware, SensorType.Load, "Some Cpu Load Name");
                        report.CpuTemperature = GetSensorValue(hardware, SensorType.Temperature, "Some Cpu Temp Name");
                        break;
                    case HardwareType.Memory:
                        report.RamUsage = GetSensorValue(hardware, SensorType.Load);
                        break;
                    case HardwareType.GpuNvidia:
                        report.GpuUsage = GetSensorValue(hardware, SensorType.Load, "Some Gpu Load Name");
                        report.GpuTemperature = GetSensorValue(hardware, SensorType.Temperature, "Some Gpu Temp Name");
                        break;
                }
            }
            return report;
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
