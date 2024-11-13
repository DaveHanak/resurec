using resurec.ResourceMonitors;
using resurec.Services.RecordingProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Services.RecordingCreators
{
    public class DatabaseRecordingCreator : IRecordingCreator
    {
        private readonly ResourceMonitor _resourceMonitor;

        public async Task StartRecording()
        {
            await _resourceMonitor.StartRecording();
        }

        public async Task StopRecording()
        {
            Recording recording = _resourceMonitor.StopRecording();
            // save recording etc
        }
    }
}
