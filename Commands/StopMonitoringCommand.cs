﻿using resurec.Models;
using resurec.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Commands
{
    public class StopMonitoringCommand : CommandBase
    {
        private readonly ResurecViewModel? _resurecViewModel;
        private readonly GlobalTimer? _globalTimer;

        public StopMonitoringCommand(ResurecViewModel? resurecViewModel, GlobalTimer? globalTimer)
        {
            _resurecViewModel = resurecViewModel;
            _globalTimer = globalTimer;

            if (_resurecViewModel != null)
            {
                _resurecViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _resurecViewModel?.IsMonitoring ?? false;
        }

        public override void Execute(object? parameter)
        {
            if (_globalTimer == null || _resurecViewModel == null)
            {
                return;
            }

            _resurecViewModel.SubmitErrorMessage = string.Empty;
            _resurecViewModel.IsSubmitting = true;

            try
            {
                _globalTimer.Stop();
                _resurecViewModel.IsMonitoring = false;
            }
            catch (Exception e)
            {
                _resurecViewModel.SubmitErrorMessage = e.Message;
            }

            _resurecViewModel.IsSubmitting = false;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResurecViewModel.IsMonitoring))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
