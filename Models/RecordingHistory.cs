using resurec.Services.RecordingEditors;
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
        private readonly IRecordingEditor _recordingEditor;
        private readonly IRecordingRemover _recordingRemover;
        
        public RecordingHistory(IRecordingProvider recordingProvider, IRecordingEditor recordingEditor, IRecordingRemover recordingRemover)
        {
            _recordingProvider = recordingProvider;
            _recordingEditor = recordingEditor;
            _recordingRemover = recordingRemover;
        }

        public async Task<IEnumerable<Recording>> GetRecordings()
        {
            return await _recordingProvider.GetRecordings();
        }

        public async Task RemoveRecording(Guid id)
        {
            await _recordingRemover.RemoveRecording(id);
        }
    }
}
