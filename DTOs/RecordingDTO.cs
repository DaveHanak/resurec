using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Models;

namespace resurec.DTOs
{
    public class RecordingDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public HardwareReportDTO HardwareReport { get; set; } = new HardwareReportDTO();

        public RecordingDTO() { }
        public RecordingDTO(Recording recording)
        {
            Name = recording.Name;
            StartTime = recording.StartTime;
            EndTime = recording.EndTime;
            Duration = recording.Duration;
            HardwareReport = new HardwareReportDTO(recording.AveragedHardwareReport);
        }
    }
}
