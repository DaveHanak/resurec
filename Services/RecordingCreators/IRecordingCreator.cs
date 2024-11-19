using resurec.Models;

namespace resurec.Services.RecordingCreators
{
    public interface IRecordingCreator
    {
        Task CreateRecording(Recording recording);
    }
}
