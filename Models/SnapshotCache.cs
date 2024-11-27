using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Models.Reports;

namespace resurec.Models
{
    public class SnapshotCache
    {
        private readonly List<HardwareReport> _recordedHardwareReports = [];
        public IReadOnlyList<HardwareReport> RecordedHardwareReports => _recordedHardwareReports;
        public HardwareReport? LatestHardwareReport { get; private set; }
        public bool RecordingCaching { get; private set; }

        public SnapshotCache() { }

        public void AddSnapshot(HardwareReport hardwareReport)
        {
            LatestHardwareReport = hardwareReport;
            if (RecordingCaching)
            {
                _recordedHardwareReports.Add(hardwareReport);
            }
        }

        public void StartRecording()
        {
            RecordingCaching = true;
        }

        public void StopRecording()
        {
            RecordingCaching = false;
        }

        public void Clear()
        {
            _recordedHardwareReports.Clear();
        }
    }
}
