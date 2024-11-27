using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using resurec.Commands;
using resurec.Models;
using resurec.Services;
using resurec.Stores;

namespace resurec.ViewModels
{
    public class ResurecViewModel : ViewModelBase, IDisposable
    {
        private readonly Recorder _recorder;
        private readonly GlobalTimer _globalTimer;

        private ObservableCollection<float> _cpuUsage;
        private ObservableCollection<float> _cpuTemperature;
        private ObservableCollection<float> _ramUsage;
        private ObservableCollection<float> _gpuUsage;
        private ObservableCollection<float> _gpuTemperature;

        public ISeries[] CpuUsageSeries { get; set; }
        public ISeries[] CpuTemperatureSeries { get; set; }
        public ISeries[] RamUsageSeries { get; set; }
        public ISeries[] GpuUsageSeries { get; set; }
        public ISeries[] GpuTemperatureSeries { get; set; }

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

            _cpuUsage = new ObservableCollection<float>(new float[60]);
            _cpuTemperature = new ObservableCollection<float>(new float[60]);
            _ramUsage = new ObservableCollection<float>(new float[60]);
            _gpuUsage = new ObservableCollection<float>(new float[60]);
            _gpuTemperature = new ObservableCollection<float>(new float[60]);

            CpuUsageSeries = [ new LineSeries<float> { Values = _cpuUsage, Name = "CPU Usage" }, new LineSeries<float> { Values = _gpuUsage, Name = "GPU Usage" }];
            CpuTemperatureSeries = [ new LineSeries<float> { Values = _cpuTemperature, Name = "CPU Temperature" }];
            RamUsageSeries = [ new LineSeries<float> { Values = _ramUsage, Name = "RAM usage" }];
            GpuUsageSeries = [ new LineSeries<float> { Values = _gpuUsage, Name = "GPU Usage" }];
            GpuTemperatureSeries = [ new LineSeries<float> { Values = _gpuTemperature, Name = "GPU Temperature" }];
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

        private void UpdateCollection(ObservableCollection<float> collection, float? value)
        {
            collection.Add(value ?? 0.0f);
            if (collection.Count > 60)
            {
                collection.RemoveAt(0);
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
            UpdateCollection(_cpuTemperature, snapshot.CpuTemperature);
            UpdateCollection(_ramUsage, snapshot.RamUsage);
            UpdateCollection(_gpuUsage, snapshot.GpuUsage);
            UpdateCollection(_gpuTemperature, snapshot.GpuTemperature);
        }

        public override void Dispose()
        {
            _globalTimer.RemoveCallback(UpdateStatistics);
        }
    }
}
