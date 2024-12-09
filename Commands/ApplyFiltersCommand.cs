using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.ViewModels;

namespace resurec.Commands
{
    public class ApplyFiltersCommand : CommandBase
    {
        private readonly RecordingHistoryViewModel? _recordingHistoryViewModel;

        public ApplyFiltersCommand(RecordingHistoryViewModel? recordingHistoryViewModel)
        {
            _recordingHistoryViewModel = recordingHistoryViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_recordingHistoryViewModel == null)
            {
                return;
            }

            _recordingHistoryViewModel.ApplyFilters();
        }
    }
}
