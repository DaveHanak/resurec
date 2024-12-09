using resurec.Stores;
using resurec.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Commands
{
    public class ClearMonitorCommand : CommandBase
    {
        private readonly ResurecViewModel? _resurecViewModel;

        public ClearMonitorCommand(ResurecViewModel? resurecViewModel)
        {
            _resurecViewModel = resurecViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_resurecViewModel == null)
            {
                return;
            }

            _resurecViewModel.SubmitErrorMessage = string.Empty;
            _resurecViewModel.IsSubmitting = true;

            try
            {
                _resurecViewModel.ClearMonitor();
            }
            catch (Exception e)
            {
                _resurecViewModel.SubmitErrorMessage = e.Message;
            }

            _resurecViewModel.IsSubmitting = false;
        }
    }
}
