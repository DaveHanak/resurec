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

        public IEnumerable<RecordingViewModel> Recordings => _recordings;

        public bool HasRecordings => _recordings.Any();

        private string? _errorMessage;
        public string ErrorMessage
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
        public ICommand NavigateCommand { get; }
        public RecordingHistoryViewModel(RecorderStore recorderStore, NavigationService<ResurecViewModel> resurecNavigationService)
        {
            _recorderStore = recorderStore;
            _recordings = [];

            LoadRecordingsCommand = new LoadRecordingsCommand(this, recorderStore);
            NavigateCommand = new NavigateCommand<ResurecViewModel>(resurecNavigationService);

            _recorderStore.RecordingMade += OnRecordingMade;
            _recordings.CollectionChanged += OnRecordingsChanged;
        }

        private void OnRecordingMade(Recording recording)
        {
            RecordingViewModel recordingViewModel = new(recording, _recorderStore);
            _recordings.Add(recordingViewModel);
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
        }

        private void OnRecordingsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasRecordings));
        }

        public override void Dispose()
        {
            _recorderStore.RecordingMade -= OnRecordingMade;
            base.Dispose();
        }
    }
}
