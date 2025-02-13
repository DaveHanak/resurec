﻿using resurec.Exceptions;
using resurec.Models.Reports;
using resurec.Models.ResourceMonitors;
using resurec.Services.RecordingCreators;

namespace resurec.Models
{
    public class Recorder : IDisposable
    {
        private readonly IRecordingCreator _recordingCreator;
        private readonly RecordingHistory _recordingHistory;
        private readonly SnapshotCache _snapshotCache;

        private readonly HardwareMonitor _hardwareMonitor;

        private readonly GlobalTimer _globalTimer;
        private DateTime _timeStarted;

        public bool IsRecording => _snapshotCache.RecordingCaching;

        public Recorder(IRecordingCreator recordingCreator, HardwareMonitor hardwareMonitor, RecordingHistory recordingHistory, SnapshotCache recordingCache, GlobalTimer globalTimer)
        {
            _recordingCreator = recordingCreator;
            _hardwareMonitor = hardwareMonitor;
            _snapshotCache = recordingCache;
            _recordingHistory = recordingHistory;
            _globalTimer = globalTimer;
            _globalTimer.AddCallback(TakeSnapshot);
        }
        
        public async Task<IEnumerable<Recording>> GetRecordings()
        {
            return await _recordingHistory.GetRecordings();
        }

        private void TakeSnapshot(object? sender, EventArgs eventArgs)
        {
            _snapshotCache.AddSnapshot(_hardwareMonitor.CreateReport());
        }

        public HardwareReport? GetLatestSnapshot()
        {
            return _snapshotCache.LatestHardwareReport;
        }

        public void StartRecording()
        {
            if (IsRecording)
            {
                throw new AlreadyRecordingException();
            }
            _snapshotCache.StartRecording();
            _timeStarted = DateTime.Now;
        }

        public void CancelRecording()
        {
            if (!IsRecording)
            {
                throw new NotRecordingException();
            }
            _snapshotCache.StopRecording();
            _snapshotCache.Clear();
        }

        public async Task<Recording> StopRecording()
        {
            if (!IsRecording)
            {
                throw new NotRecordingException();
            }

            var timeStopped = DateTime.Now;
            var duration = timeStopped - _timeStarted;
            if (duration.TotalSeconds < 2)
            {
                throw new StoppedRecordingTooSoonException();
            }
            _snapshotCache.StopRecording();
            var finalHardwareReport = new AveragedHardwareReport(_snapshotCache.RecordedHardwareReports);
            _snapshotCache.Clear();

            var recording = new Recording(
                Guid.NewGuid(),
                _timeStarted.ToString("yyyy-MM-dd HH:mm:ss"),
                _timeStarted,
                timeStopped,
                timeStopped.Subtract(_timeStarted),
                finalHardwareReport);
            await _recordingCreator.CreateRecording(recording);
            
            return recording;
        }
        
        public async Task EditRecording(Guid id, string name)
        {
            await _recordingHistory.EditRecording(id, name);
        }

        public async Task RemoveRecording(Guid id)
        {
            await _recordingHistory.RemoveRecording(id);
        }

        public void Dispose()
        {
            _globalTimer.RemoveCallback(TakeSnapshot);
        }
    }
}
