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
        private readonly RecordingHistoryViewModel _viewModel;
        private readonly RecorderStore _recorderStore;

        public LoadRecordingsCommand(RecordingHistoryViewModel viewModel, RecorderStore recorderStore)
        {
            _viewModel = viewModel;
            _recorderStore = recorderStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;

            try
            {
                await _recorderStore.Load();

                _viewModel.UpdateRecordings(_recorderStore.Recordings);
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to load reservations.";
            }

            _viewModel.IsLoading = false;
        }
    }
}
