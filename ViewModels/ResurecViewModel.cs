using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using resurec.Commands;
using resurec.Models;
using resurec.Services;
using resurec.Stores;
using resurec.ViewModels.CustomLvc;
using SkiaSharp;

namespace resurec.ViewModels
{
    public class ResurecViewModel : ViewModelBase, IDisposable
    {
        private readonly Recorder _recorder;
        private readonly GlobalTimer _globalTimer;

        //todo move to some global color palette
        private static readonly SKColor red = new(200, 0, 0);
        private static readonly SKColor red_transparent = new(200, 0, 0, 150);
        private static readonly SKColor green = new(0, 200, 0);
        private static readonly SKColor green_transparent = new(0, 200, 0, 150);
        private static readonly SKColor blue = new(0, 0, 200);
        private static readonly SKColor blue_transparent = new(0, 0, 200, 150);


        public UsageChart CpuUsage { get; set; } = new("CPU", AxisPosition.Start, red, red_transparent);
        public UsageChart GpuUsage { get; set; } = new("GPU", AxisPosition.End, green, green_transparent);
        public UsageGauge RamUsage { get; set; } = new("%", 30, blue);
        public UsageGauge CpuTemperature { get; set; } = new("°C", 60, red);
        public UsageGauge GpuTemperature { get; set; } = new("°C", 60, green);

        public ICommand StartRecordingCommand { get; }
        public ICommand StopRecordingCommand { get; }
        public ICommand CancelRecordingCommand { get; }
        public ICommand StartMonitoringCommand { get; }
        public ICommand StopMonitoringCommand { get; }
        public ICommand ClearMonitorCommand { get; }
        public ICommand NavigateCommand { get; }

        public ResurecViewModel(Recorder recorder, RecorderStore recorderStore, NavigationService<RecordingHistoryViewModel> recordingHistoryNavigationService, GlobalTimer globalTimer)
        {
            _recorder = recorder;
            _globalTimer = globalTimer;

            StartRecordingCommand = new StartRecordingCommand(this, recorderStore);
            StopRecordingCommand = new StopRecordingCommand(this, recorderStore, recordingHistoryNavigationService);
            CancelRecordingCommand = new CancelRecordingCommand(this, recorderStore);
            StartMonitoringCommand = new StartMonitoringCommand(this, globalTimer);
            StopMonitoringCommand = new StopMonitoringCommand(this, globalTimer);
            ClearMonitorCommand = new ClearMonitorCommand(this);
            NavigateCommand = new NavigateCommand<RecordingHistoryViewModel>(recordingHistoryNavigationService);

            _globalTimer.AddCallback(UpdateStatistics);

            IsMonitoring = true;
            IsRecording = false;
        }

        private bool _isMonitoring;
        public bool IsMonitoring
        {
            get => _isMonitoring;
            set
            {
                _isMonitoring = value;
                OnPropertyChanged(nameof(IsMonitoring));
            }
        }

        private bool _isRecording;
        public bool IsRecording
        {
            get => _isRecording;
            set
            {
                _isRecording = value;
                OnPropertyChanged(nameof(IsRecording));
            }
        }

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

        private void UpdateStatistics(object? sender, EventArgs e)
        {
            if (_recorder == null)
            {
                return;
            }
            var snapshot = _recorder.GetLatestSnapshot();
            if (snapshot == null)
            {
                return;
            }

            CpuUsage.Update(snapshot.CpuUsage);
            RamUsage.Update(snapshot.RamUsage);
            GpuUsage.Update(snapshot.GpuUsage);
            CpuTemperature.Update(snapshot.CpuTemperature);
            GpuTemperature.Update(snapshot.GpuTemperature);
        }

        public void ClearMonitor()
        {
            _globalTimer.RemoveCallback(UpdateStatistics);
            CpuUsage.Clear();
            RamUsage.Clear();
            GpuUsage.Clear();
            CpuTemperature.Clear();
            GpuTemperature.Clear();
            _globalTimer.AddCallback(UpdateStatistics);
        }

        public override void Dispose()
        {
            _globalTimer.RemoveCallback(UpdateStatistics);
        }
    }
}
