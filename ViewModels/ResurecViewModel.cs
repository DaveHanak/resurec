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

        private readonly ObservableCollection<float> _cpuUsage;
        private readonly ObservableCollection<float> _ramUsage;
        private readonly ObservableCollection<float> _gpuUsage;

        public CpuUsageChart CpuChart { get; set; }
        public RamUsageChart RamChart { get; set; }
        public GpuUsageChart GpuChart { get; set; }

        public NeedleGauge CpuTemperatureGauge { get; set; }
        public NeedleGauge GpuTemperatureGauge { get; set; }

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

            _globalTimer.AddCallback(UpdateStatistics);

            CpuChart = new CpuUsageChart(_cpuUsage, _gpuUsage);
            RamChart = new RamUsageChart(_ramUsage);
            GpuChart = new GpuUsageChart(_gpuUsage);

            CpuTemperatureGauge = new NeedleGauge();
            GpuTemperatureGauge = new NeedleGauge();
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

            UpdateCollection(_cpuUsage, snapshot.CpuUsage);
            UpdateCollection(_ramUsage, snapshot.RamUsage);
            UpdateCollection(_gpuUsage, snapshot.GpuUsage);

            CpuChart.UpdateChart();
            CpuTemperatureGauge.UpdateNeedle(snapshot.CpuTemperature);
            GpuTemperatureGauge.UpdateNeedle(snapshot.GpuTemperature);
        }

        public override void Dispose()
        {
            _globalTimer.RemoveCallback(UpdateStatistics);
        }
    }
}
