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
        public required string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }

        public float CpuUsage { get; set; }
        public float CpuTemperature { get; set; }
        public float RamUsage { get; set; }
        public float GpuUsage { get; set; }
        public float GpuTemperature { get; set; }
    }
}
