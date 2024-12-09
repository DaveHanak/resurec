using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Stores;
using resurec.ViewModels;

namespace resurec.Commands
{
    public class LoadRecordingsCommand : AsyncCommandBase
    {
        private readonly RecordingHistoryViewModel _recordingHistoryViewModel;
        private readonly RecorderStore _recorderStore;

        public LoadRecordingsCommand(RecordingHistoryViewModel recordingHistoryViewModel, RecorderStore recorderStore)
        {
            _recordingHistoryViewModel = recordingHistoryViewModel;
            _recorderStore = recorderStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _recordingHistoryViewModel.ErrorMessage = string.Empty;
            _recordingHistoryViewModel.IsLoading = true;

            try
            {
                await _recorderStore.Load();

                _recordingHistoryViewModel.UpdateRecordings(_recorderStore.Recordings);
            }
            catch (Exception)
            {
                _recordingHistoryViewModel.ErrorMessage = "Failed to load recordings.";
            }

            _recordingHistoryViewModel.IsLoading = false;
        }
    }
}
