using resurec.Services.RecordingCreators;

namespace resurec.Models
{
    public class Recorder
    {
        private readonly IRecordingCreator _recordingCreator;

        private readonly RecordingHistory _recordingHistory;

        public Recorder(RecordingHistory recordingHistory)
        {
            _recordingHistory = recordingHistory;
        }
        
        /// <summary>
        /// Get all recordings.
        /// </summary>
        /// <returns>The entire recording history.</returns>
        public async Task<IEnumerable<Recording>> GetRecordingsAsync()
        {
            return await _recordingHistory.GetRecordingsAsync();
        }

        /// <summary>
        /// Starts recording resource usage data to create a Recording object with.
        /// </summary>
        public async Task StartRecording()
        {
            await _recordingCreator.StartRecording();
        }

        /// <summary>
        /// Stops recording resource usage data and saves the resulting Recording object in history.
        /// </summary>
        public async Task StopRecording()
        {
            await _recordingCreator.StopRecording();
        }
    }
}
