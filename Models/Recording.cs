using resurec.Models.Reports;

namespace resurec.Models
{
    public class Recording
    {
        public string Name => StartTime.ToString("yyyy-MM-dd HH:mm:ss");
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Duration => EndTime.Subtract(StartTime);
        public AveragedHardwareReport AveragedHardwareReport { get; }

        public Recording(DateTime startTime, DateTime endTime, AveragedHardwareReport averagedHardwareReport)
        {
            StartTime = startTime;
            EndTime = endTime;
            AveragedHardwareReport = averagedHardwareReport;
        }
    }
}
