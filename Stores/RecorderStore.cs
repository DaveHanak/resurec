﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Exceptions;
using resurec.Models;

namespace resurec.Stores
{
    public class RecorderStore
    {
        private readonly Recorder _recorder;
        private readonly List<Recording> _recordings;
        private Lazy<Task> _initializeLazy;

        public IEnumerable<Recording> Recordings => _recordings;

        public event Action<Recording>? RecordingMade;
        public event Action<Recording>? RecordingEdited;
        public event Action<Recording>? RecordingRemoved;

        public RecorderStore(Recorder recorder)
        {
            _recorder = recorder;
            _recordings = new List<Recording>();
            _initializeLazy = new Lazy<Task>(Initialize);
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }
        public void StartRecording()
        {
            if (_recorder.IsRecording)
            {
                throw new AlreadyRecordingException();
            }

            _recorder.StartRecording();
        }
        public void CancelRecording()
        {
            if (!_recorder.IsRecording)
            {
                throw new NotRecordingException();
            }

            _recorder.CancelRecording();
        }
        public async Task StopRecording()
        {
            if (!_recorder.IsRecording)
            {
                throw new NotRecordingException();
            }

            var recording = await _recorder.StopRecording();
            _recordings.Add(recording);
            OnRecordingMade(recording);
        }
        public async Task EditRecording(Guid id, string name)
        {
            var recording = _recordings.Find(r => r.Id == id) ?? throw new KeyNotFoundException("Recording not found.");
            await _recorder.EditRecording(id, name);
            recording.Name = name;
            OnRecordingEdited(recording);
        }
        public async Task RemoveRecording(Guid id)
        {
            var recording = _recordings.Find(r => r.Id == id) ?? throw new KeyNotFoundException("Recording not found.");
            await _recorder.RemoveRecording(id);
            _recordings.Remove(recording);
            OnRecordingRemoved(recording);
        }
        private void OnRecordingMade(Recording recording)
        {
            RecordingMade?.Invoke(recording);
        }
        private void OnRecordingEdited(Recording recording)
        {
            RecordingEdited?.Invoke(recording);
        }
        private void OnRecordingRemoved(Recording recording)
        {
            RecordingRemoved?.Invoke(recording);
        }

        private async Task Initialize()
        {
            IEnumerable<Recording> recordings = await _recorder.GetRecordings();
            _recordings.Clear();
            _recordings.AddRange(recordings);
        }
    }
}
