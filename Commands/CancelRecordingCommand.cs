using resurec.Models;
using resurec.Services;
using resurec.Stores;
using resurec.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Commands
{
    public class CancelRecordingCommand : CommandBase
    {
        private readonly ResurecViewModel? _resurecViewModel;
        private readonly RecorderStore? _recorderStore;

        public CancelRecordingCommand(ResurecViewModel? resurecViewModel, RecorderStore? recorderStore)
        {
            _resurecViewModel = resurecViewModel;
            _recorderStore = recorderStore;

            if (_resurecViewModel != null)
            {
                _resurecViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _resurecViewModel?.IsRecording ?? false;
        }

        public override void Execute(object? parameter)
        {
            if (_recorderStore == null || _resurecViewModel == null)
            {
                return;
            }

            _resurecViewModel.SubmitErrorMessage = string.Empty;
            _resurecViewModel.IsSubmitting = true;

            try
            {
                _recorderStore.CancelRecording();
                _resurecViewModel.IsRecording = false;
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
