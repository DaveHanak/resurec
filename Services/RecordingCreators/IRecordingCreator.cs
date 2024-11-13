namespace resurec.Services.RecordingCreators
{
    public interface IRecordingCreator
    {
        Task StartRecording();
        Task StopRecording();
    }
}
