using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using resurec.Commands;
using resurec.Models;
using resurec.Services;
using resurec.Stores;

namespace resurec.ViewModels
{
    public class ResurecViewModel : ViewModelBase, IDisposable
    {
        private readonly Recorder? _recorder;
        private readonly GlobalTimer _globalTimer;
        public ICommand StartRecordingCommand { get; }
        public ICommand StopRecordingCommand { get; }
        public ICommand NavigateCommand { get; }
        public ResurecViewModel(Recorder recorder, RecorderStore recorderStore, NavigationService<RecordingHistoryViewModel> recordingHistoryNavigationService, GlobalTimer globalTimer)
        {
            _recorder = recorder;
            _globalTimer = globalTimer;

            StartRecordingCommand = new StartRecordingCommand(this, recorderStore);
            StopRecordingCommand = new StopRecordingCommand(this, recorderStore, recordingHistoryNavigationService);
            NavigateCommand = new NavigateCommand<RecordingHistoryViewModel>(recordingHistoryNavigationService);

            _globalTimer.AddCallback(UpdateHardwareReport);
        }
        public bool IsRecording => _recorder?.IsRecording ?? false;

        private string? _submitErrorMessage;
        public string SubmitErrorMessage
        {
            get => _submitErrorMessage ?? string.Empty;
            set
            {
                _submitErrorMessage = value;
                OnPropertyChanged(nameof(SubmitErrorMessage));

                OnPropertyChanged(nameof(HasSubmitErrorMessage));
            }
        }

        public bool HasSubmitErrorMessage => !string.IsNullOrEmpty(SubmitErrorMessage);

        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get => _isSubmitting;
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        private HardwareReportViewModel? _hardwareReport;
        public HardwareReportViewModel? HardwareReport
        {
            get => _hardwareReport;
            set
            {
                _hardwareReport = value;
                OnPropertyChanged(nameof(HardwareReport));
            }
        }

        private void UpdateHardwareReport(object? sender, EventArgs e)
        {
            if (_recorder == null)
            {
                return;
            }
            HardwareReport = new HardwareReportViewModel(_recorder.GetLatestSnapshot());
        }

        public override void Dispose()
        {
            _globalTimer.RemoveCallback(UpdateHardwareReport);
        }
    }
}
