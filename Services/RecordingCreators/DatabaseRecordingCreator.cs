using resurec.Models;
using resurec.Models.Reports;
using resurec.ResourceMonitors;
using resurec.Services.RecordingProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace resurec.Services.RecordingCreators
{
    public class DatabaseRecordingCreator : IRecordingCreator
    {
        private readonly HardwareMonitor _hardwareMonitor;
        private readonly SoftwareMonitor _softwareMonitor;

        private readonly List<HardwareReport> _hardwareReports;
        private readonly List<SoftwareReport> _softwareReports;

        private readonly DispatcherTimer _timer;
        private DateTime _timeStarted;
        private bool _isStarted = false;

        public DatabaseRecordingCreator(HardwareMonitor hardwareMonitor, SoftwareMonitor softwareMonitor, DispatcherTimer timer)
        {
            _hardwareMonitor = hardwareMonitor;
            _softwareMonitor = softwareMonitor;
            _timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += TakeSnapshot;
        }

        private void TakeSnapshot(object? sender, EventArgs eventArgs)
        {

        }

        public async Task StartRecording()
        {
            if (_isStarted)
            {
                throw new InvalidOperationException("already started recording");
            }
            _isStarted = true;
            _timeStarted = DateTime.Now;
            _timer.Start();
            
        }

        public async Task StopRecording()
        {
            if (!_isStarted)
            {
                throw new InvalidOperationException("nothing to stop");
            }
            _timer.Stop();
            var timeStopped = DateTime.Now;
            _isStarted = false;

            var duration = timeStopped - _timeStarted;

            HardwareReport finalHardwareReport = GetAveragedReport();
            SoftwareReport finalSoftwareReport = new SoftwareReport(); // placeholder
            Recording recording = new Recording(1, "name", "desc", _timeStarted, timeStopped, finalHardwareReport, finalSoftwareReport);
            // save recording etc
        }

        private HardwareReport GetAveragedReport()
        {
            var sumCpuUsage = 0.0f;
            var sumCpuTemperature = 0.0f;
            var sumRamUsage = 0.0f;
            var sumGpuUsage = 0.0f;
            var sumGpuTemperature = 0.0f;
            foreach (var report in _hardwareReports)
            {
                sumCpuUsage += report.CpuUsage ?? 0.0f;
                sumCpuTemperature += report.CpuTemperature ?? 0.0f;
                sumRamUsage += report.RamUsage ?? 0.0f;
                sumGpuUsage += report.GpuUsage ?? 0.0f;
                sumGpuTemperature += report.GpuTemperature ?? 0.0f;
            }
            var count = _hardwareReports.Count;
            var avgCpuUsage = sumCpuUsage / count;
            var avgCpuTemperature = sumCpuTemperature / count;
            var avgRamUsage = sumRamUsage / count;
            var avgGpuUsage = sumGpuUsage / count;
            var avgGpuTemperature = sumGpuTemperature / count;
            return new HardwareReport(avgCpuUsage, avgCpuTemperature, avgRamUsage, avgGpuUsage, avgGpuTemperature);
        }
    }
}
