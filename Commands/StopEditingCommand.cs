﻿using resurec.Stores;
using resurec.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Commands
{
    public class StopEditingCommand : AsyncCommandBase
    {
        private readonly RecordingViewModel? _recordingViewModel;
        private readonly RecorderStore? _recorderStore;

        public StopEditingCommand(RecordingViewModel? recordingViewModel, RecorderStore? recorderStore)
        {
            _recordingViewModel = recordingViewModel;
            _recorderStore = recorderStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (_recorderStore == null || _recordingViewModel == null)
            {
                return;
            }

            _recordingViewModel.IsEditing = false;

            await _recorderStore.EditRecording(_recordingViewModel.Id, _recordingViewModel.Name);
        }
    }
}
