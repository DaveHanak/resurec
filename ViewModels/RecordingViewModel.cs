using resurec.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ViewModels
{
    public class RecordingViewModel
    {
        private readonly Recording _recording;

        public string Id => _recording.Id.ToString();
        public string Name => _recording.Name;
        public string Description => _recording.Description;
        public string StartTime => _recording.StartTime.ToString("u");
        public string EndTime => _recording.StartTime.ToString("u");
        public string Duration => _recording.Duration.ToString("mm\\:ss");
        public HardwareReportViewModel HardwareReport { get; }
        public SoftwareReportViewModel SoftwareReport { get; }

        public RecordingViewModel(Recording recording)
        {
            _recording = recording;
            HardwareReport = new HardwareReportViewModel(recording.HardwareReport);
            SoftwareReport = new SoftwareReportViewModel(recording.SoftwareReport);
        }
    }
}
