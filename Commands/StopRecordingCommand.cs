using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Exceptions;
using resurec.Services;
using resurec.Stores;
using resurec.ViewModels;

namespace resurec.Commands
{
    public class StopRecordingCommand : AsyncCommandBase
    {
        private readonly ResurecViewModel? _resurecViewModel;
        private readonly RecorderStore? _recorderStore;
        private readonly NavigationService<RecordingHistoryViewModel> _recordingHistoryNavigationService;


        public StopRecordingCommand(ResurecViewModel? resurecViewModel, RecorderStore? recorderStore, NavigationService<RecordingHistoryViewModel> recordingHistoryNavigationService)
        {
            _resurecViewModel = resurecViewModel;
            _recorderStore = recorderStore;
            _recordingHistoryNavigationService = recordingHistoryNavigationService;

            if (_resurecViewModel != null)
            {
                _resurecViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _resurecViewModel?.IsRecording ?? false;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (_recorderStore == null || _resurecViewModel == null)
            {
                return;
            }

            _resurecViewModel.SubmitErrorMessage = string.Empty;
            _resurecViewModel.IsSubmitting = true;

            try
            {
                await _recorderStore.StopRecording();
                _resurecViewModel.IsRecording = false;
                _recordingHistoryNavigationService.Navigate();
            }
            catch (Exception e)
            {
                _resurecViewModel.SubmitErrorMessage = e.Message;
            }

            _resurecViewModel.IsSubmitting = false;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResurecViewModel.IsRecording))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
