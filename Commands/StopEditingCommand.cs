using resurec.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Commands
{
    public class StopEditingCommand : CommandBase
    {
        private readonly RecordingViewModel? _recordingViewModel;

        public StopEditingCommand(RecordingViewModel? recordingViewModel)
        {
            _recordingViewModel = recordingViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_recordingViewModel == null)
            {
                return;
            }

            _recordingViewModel.IsEditing = false;
        }
    }
}
