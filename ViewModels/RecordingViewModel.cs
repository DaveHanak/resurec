using resurec.Commands;
using resurec.Models;
using resurec.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace resurec.ViewModels
{
    public class RecordingViewModel : ViewModelBase
    {
        private readonly Recording _recording;
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string StartTime => _recording.StartTime.ToString("HH:mm:ss");
        public string EndTime => _recording.EndTime.ToString("HH:mm:ss");
        public string Duration => _recording.Duration.ToString("mm\\:ss");
        public HardwareReportViewModel HardwareReport { get; }

        public ICommand StartEditingCommand { get; }
        public ICommand StopEditingCommand { get; }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        public RecordingViewModel(Recording recording)
        {
            _recording = recording;
            _name = recording.Name;
            HardwareReport = new HardwareReportViewModel(recording.AveragedHardwareReport);

            StartEditingCommand = new StartEditingCommand(this);
            StopEditingCommand = new StopEditingCommand(this);
        }
    }
}
