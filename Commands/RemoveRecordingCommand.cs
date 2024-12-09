using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Stores;
using resurec.ViewModels;

namespace resurec.Commands
{
    public class RemoveRecordingCommand : AsyncCommandBase
    {
        private readonly RecordingHistoryViewModel? _recordingHistoryViewModel;
        private readonly RecorderStore? _recorderStore;

        public RemoveRecordingCommand(RecordingHistoryViewModel? recordingHistoryViewModel, RecorderStore? recorderStore)
        {
            _recordingHistoryViewModel = recordingHistoryViewModel;
            _recorderStore = recorderStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter == null || _recorderStore == null || _recordingHistoryViewModel == null)
            {
                return;
            }
            RecordingViewModel recording = (RecordingViewModel)parameter;
            await _recorderStore.RemoveRecording(recording.Id);
        }
    }
}
