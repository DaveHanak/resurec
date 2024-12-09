using resurec.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Commands
{
    public class StartEditingCommand : CommandBase
    {
        private readonly RecordingViewModel? _recordingViewModel;

        public StartEditingCommand(RecordingViewModel? recordingViewModel)
        {
            _recordingViewModel = recordingViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_recordingViewModel == null)
            {
                return;
            }

            _recordingViewModel.IsEditing = true;
        }
    }
}
