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
    public class ClearMonitorCommand : CommandBase
    {
        private readonly ResurecViewModel? _resurecViewModel;

        public ClearMonitorCommand(ResurecViewModel? resurecViewModel)
        {
            _resurecViewModel = resurecViewModel;

            if (_resurecViewModel != null)
            {
                _resurecViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (_resurecViewModel == null)
            {
                return;
            }

            _resurecViewModel.SubmitErrorMessage = string.Empty;
            _resurecViewModel.IsSubmitting = true;

            try
            {
                _resurecViewModel.ClearMonitor();
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
