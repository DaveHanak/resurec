using resurec.Models.Reports;

namespace resurec.Models
{
    public class Recording
    {
        public int Id { get; } //?
        public string Name { get; }//?
        public string Description { get; }//?
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Duration => EndTime.Subtract(StartTime);
        public HardwareReport HardwareReport { get; }
        public SoftwareReport SoftwareReport { get; }

        public Recording(int id, string name, string description, DateTime startTime, DateTime endTime, HardwareReport hardwareReport, SoftwareReport softwareReport)
        {
            Id = id;
            Name = name;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            HardwareReport = hardwareReport;
            SoftwareReport = softwareReport;
        }
    }
}
