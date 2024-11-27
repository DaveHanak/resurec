using resurec.Services.RecordingProviders;
using resurec.Services.RecordingRemovers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Models
{
    public class RecordingHistory
    {
        private readonly IRecordingProvider _recordingProvider;
        private readonly IRecordingRemover _recordingRemover;
        
        public RecordingHistory(IRecordingProvider recordingProvider, IRecordingRemover recordingRemover)
        {
            _recordingProvider = recordingProvider;
            _recordingRemover = recordingRemover;
        }

        public async Task<IEnumerable<Recording>> GetRecordings()
        {
            return await _recordingProvider.GetRecordings();
        }

        public async Task RemoveRecording(string recordingName)
        {
            await _recordingRemover.RemoveRecording(recordingName);
        }
    }
}
