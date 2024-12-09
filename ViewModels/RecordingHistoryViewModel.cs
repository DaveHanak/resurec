using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    public class RecordingHistoryViewModel : ViewModelBase
    {
        private readonly RecorderStore _recorderStore;

        private readonly ObservableCollection<RecordingViewModel> _recordings;
        private readonly ObservableCollection<RecordingViewModel> _filteredRecordings;

        public IEnumerable<RecordingViewModel> Recordings => _filteredRecordings;
        public bool HasRecordings => _filteredRecordings.Any();

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get
            {
                return _errorMessage ?? string.Empty;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public ICommand LoadRecordingsCommand { get; }
        public ICommand RemoveRecordingCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand ApplyFiltersCommand { get; }
        public RecordingHistoryViewModel(RecorderStore recorderStore, NavigationService<ResurecViewModel> resurecNavigationService)
        {
            _recorderStore = recorderStore;
            _recordings = [];
            _filteredRecordings = [];

            LoadRecordingsCommand = new LoadRecordingsCommand(this, recorderStore);
            NavigateCommand = new NavigateCommand<ResurecViewModel>(resurecNavigationService);
            RemoveRecordingCommand = new RemoveRecordingCommand(this, recorderStore);
            ApplyFiltersCommand = new ApplyFiltersCommand(this);

            _recorderStore.RecordingMade += OnRecordingMade;
            _recorderStore.RecordingEdited += OnRecordingEdited;
            _recorderStore.RecordingRemoved += OnRecordingRemoved;
            _recordings.CollectionChanged += OnRecordingsChanged;
        }

        private void OnRecordingMade(Recording recording)
        {
            RecordingViewModel recordingViewModel = new(recording, _recorderStore);
            _recordings.Add(recordingViewModel);
            ApplyFilters();
        }
        private void OnRecordingEdited(Recording recording)
        {
            ApplyFilters();
        }
        private void OnRecordingRemoved(Recording recording)
        {
            RecordingViewModel? recordingViewModel = _recordings.FirstOrDefault(r => r.Id == recording.Id);
            if (recordingViewModel != null)
            {
                _recordings.Remove(recordingViewModel);
                ApplyFilters();
            }
        }

        public static RecordingHistoryViewModel LoadViewModel(RecorderStore recorderStore, NavigationService<ResurecViewModel> resurecNavigationService)
        {
            RecordingHistoryViewModel viewModel = new(recorderStore, resurecNavigationService);

            viewModel.LoadRecordingsCommand.Execute(null);

            return viewModel;
        }

        public void UpdateRecordings(IEnumerable<Recording> recordings)
        {
            _recordings.Clear();

            foreach (Recording recording in recordings)
            {
                RecordingViewModel recordingViewModel = new(recording, _recorderStore);
                _recordings.Add(recordingViewModel);
            }
            ApplyFilters();
        }

        private void OnRecordingsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasRecordings));
        }

        public override void Dispose()
        {
            _recorderStore.RecordingMade -= OnRecordingMade;
            _recorderStore.RecordingEdited -= OnRecordingEdited;
            _recorderStore.RecordingRemoved -= OnRecordingRemoved;
            _recordings.CollectionChanged -= OnRecordingsChanged;

            base.Dispose();
        }

        // Filter properties
        public void ApplyFilters()
        {
            ErrorMessage = null;
            try
            {
                var filtered =
                from r in _recordings
                where (string.IsNullOrEmpty(NameFilter) || r.Name.Contains(NameFilter, StringComparison.OrdinalIgnoreCase))
                && (!StartTimeFilter.HasValue || DateTime.Parse(r.StartTime) >= StartTimeFilter.Value)
                && (!EndTimeFilter.HasValue || DateTime.Parse(r.EndTime) <= EndTimeFilter.Value)
                && (string.IsNullOrEmpty(DurationFilterFrom) || TimeSpan.Parse(r.Duration) >= TimeSpan.Parse(DurationFilterFrom))
                && (string.IsNullOrEmpty(DurationFilterTo) || TimeSpan.Parse(r.Duration) <= TimeSpan.Parse(DurationFilterTo))
                && (string.IsNullOrEmpty(CpuUsageFilterFrom) || r.HardwareReport.GetCpuUsage() >= float.Parse(CpuUsageFilterFrom))
                && (string.IsNullOrEmpty(CpuUsageFilterTo) || r.HardwareReport.GetCpuUsage() <= float.Parse(CpuUsageFilterTo))
                && (string.IsNullOrEmpty(GpuUsageFilterFrom) || r.HardwareReport.GetGpuUsage() >= float.Parse(GpuUsageFilterFrom))
                && (string.IsNullOrEmpty(GpuUsageFilterTo) || r.HardwareReport.GetGpuUsage() <= float.Parse(GpuUsageFilterTo))
                && (string.IsNullOrEmpty(RamUsageFilterFrom) || r.HardwareReport.GetRamUsage() >= float.Parse(RamUsageFilterFrom))
                && (string.IsNullOrEmpty(RamUsageFilterTo) || r.HardwareReport.GetRamUsage() <= float.Parse(RamUsageFilterTo))
                && (string.IsNullOrEmpty(CpuTemperatureFilterFrom) || r.HardwareReport.GetCpuTemperature() >= float.Parse(CpuTemperatureFilterFrom))
                && (string.IsNullOrEmpty(CpuTemperatureFilterTo) || r.HardwareReport.GetCpuTemperature() <= float.Parse(CpuTemperatureFilterTo))
                && (string.IsNullOrEmpty(GpuTemperatureFilterFrom) || r.HardwareReport.GetGpuTemperature() >= float.Parse(GpuTemperatureFilterFrom))
                && (string.IsNullOrEmpty(GpuTemperatureFilterTo) || r.HardwareReport.GetGpuTemperature() <= float.Parse(GpuTemperatureFilterTo))
                select r;

                _filteredRecordings.Clear();

                foreach (var recording in filtered)
                {
                    _filteredRecordings.Add(recording);
                }

                OnPropertyChanged(nameof(Recordings));
                OnPropertyChanged(nameof(HasRecordings));
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
        private bool HasStartTimeBeforeEndTime => StartTimeFilter < EndTimeFilter;

        private string? _nameFilter;
        public string? NameFilter
        {
            get => _nameFilter;
            set
            {
                _nameFilter = value;
                OnPropertyChanged(nameof(NameFilter));
            }
        }

        private DateTime? _startTimeFilter;
        public DateTime? StartTimeFilter
        {
            get => _startTimeFilter;
            set
            {
                _startTimeFilter = value;
                if(!HasStartTimeBeforeEndTime)
                {
                    ErrorMessage = "Start time must be before end time.";
                }
                else
                {
                    ErrorMessage = null;
                }
                OnPropertyChanged(nameof(StartTimeFilter));
            }
        }
        private DateTime? _endTimeFilter;
        public DateTime? EndTimeFilter
        {
            get => _endTimeFilter;
            set
            {
                _endTimeFilter = value;
                if (!HasStartTimeBeforeEndTime)
                {
                    ErrorMessage = "Start time must be before end time.";
                }
                else
                {
                    ErrorMessage = null;
                }
                OnPropertyChanged(nameof(EndTimeFilter));
            }
        }


        private string? _durationFilterFrom;
        public string? DurationFilterFrom
        {
            get => _durationFilterFrom;
            set
            {
                _durationFilterFrom = value;
                OnPropertyChanged(nameof(DurationFilterFrom));
            }
        }

        private string? _durationFilterTo;
        public string? DurationFilterTo
        {
            get => _durationFilterTo;
            set
            {
                _durationFilterTo = value;
                OnPropertyChanged(nameof(DurationFilterTo));
            }
        }

        private string? _cpuUsageFilterFrom;
        public string? CpuUsageFilterFrom
        {
            get => _cpuUsageFilterFrom;
            set
            {
                _cpuUsageFilterFrom = value;
                OnPropertyChanged(nameof(CpuUsageFilterFrom));
            }
        }

        private string? _cpuUsageFilterTo;
        public string? CpuUsageFilterTo
        {
            get => _cpuUsageFilterTo;
            set
            {
                _cpuUsageFilterTo = value;
                OnPropertyChanged(nameof(CpuUsageFilterTo));
            }
        }

        private string? _gpuUsageFilterFrom;
        public string? GpuUsageFilterFrom
        {
            get => _gpuUsageFilterFrom;
            set
            {
                _gpuUsageFilterFrom = value;
                OnPropertyChanged(nameof(GpuUsageFilterFrom));
            }
        }

        private string? _gpuUsageFilterTo;
        public string? GpuUsageFilterTo
        {
            get => _gpuUsageFilterTo;
            set
            {
                _gpuUsageFilterTo = value;
                OnPropertyChanged(nameof(GpuUsageFilterTo));
            }
        }

        private string? _ramUsageFilterFrom;
        public string? RamUsageFilterFrom
        {
            get => _ramUsageFilterFrom;
            set
            {
                _ramUsageFilterFrom = value;
                OnPropertyChanged(nameof(RamUsageFilterFrom));
            }
        }

        private string? _ramUsageFilterTo;
        public string? RamUsageFilterTo
        {
            get => _ramUsageFilterTo;
            set
            {
                _ramUsageFilterTo = value;
                OnPropertyChanged(nameof(RamUsageFilterTo));
            }
        }

        private string? _cpuTemperatureFilterFrom;
        public string? CpuTemperatureFilterFrom
        {
            get => _cpuTemperatureFilterFrom;
            set
            {
                _cpuTemperatureFilterFrom = value;
                OnPropertyChanged(nameof(CpuTemperatureFilterFrom));
            }
        }

        private string? _cpuTemperatureFilterTo;
        public string? CpuTemperatureFilterTo
        {
            get => _cpuTemperatureFilterTo;
            set
            {
                _cpuTemperatureFilterTo = value;
                OnPropertyChanged(nameof(CpuTemperatureFilterTo));
            }
        }

        private string? _gpuTemperatureFilterFrom;
        public string? GpuTemperatureFilterFrom
        {
            get => _gpuTemperatureFilterFrom;
            set
            {
                _gpuTemperatureFilterFrom = value;
                OnPropertyChanged(nameof(GpuTemperatureFilterFrom));
            }
        }

        private string? _gpuTemperatureFilterTo;
        public string? GpuTemperatureFilterTo
        {
            get => _gpuTemperatureFilterTo;
            set
            {
                _gpuTemperatureFilterTo = value;
                OnPropertyChanged(nameof(GpuTemperatureFilterTo));
            }
        }
    }
}
