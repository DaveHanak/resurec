using resurec.Models;

namespace resurec.Services.RecordingProviders
{
    public interface IRecordingProvider
    {
        Task<IEnumerable<Recording>> GetRecordings();
    }
}
