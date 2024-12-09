using resurec.Models.Reports;

namespace resurec.Models
{
    public class Recording
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Duration { get; }
        public AveragedHardwareReport AveragedHardwareReport { get; }

        public Recording(
            Guid id,
            string name,
            DateTime startTime,
            DateTime endTime,
            TimeSpan duration,
            AveragedHardwareReport averagedHardwareReport)
        {
            Id = id;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            AveragedHardwareReport = averagedHardwareReport;
        }
    }
}
