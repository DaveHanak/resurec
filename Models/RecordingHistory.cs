using resurec.Services.RecordingProviders;
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
        
        public RecordingHistory(IRecordingProvider recordingProvider)
        {
            _recordingProvider = recordingProvider;
        }

        public async Task<IEnumerable<Recording>> GetRecordingsAsync()
        {
            return await _recordingProvider.GetRecordings();
        }

        public async Task AddRecording(Recording recording)
        {
           // await _recordingProvider.AddRecording(recording);
           throw new NotImplementedException();
        }

        public async Task RemoveRecording(Recording recording)
        {
            //await _recordingProvider.RemoveRecording(recording);
            throw new NotImplementedException();
        }
    }
}
