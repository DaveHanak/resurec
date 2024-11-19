using resurec.DbContexts;
using resurec.DbContexts.Factories;
using resurec.DTOs;
using resurec.Models;
using resurec.Models.Reports;
using resurec.Models.ResourceMonitors;
using resurec.Services.RecordingProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace resurec.Services.RecordingCreators
{
    public class DatabaseRecordingCreator : IRecordingCreator
    {
        private readonly IResurecDbContextFactory _dbContextFactory;

        public DatabaseRecordingCreator(IResurecDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateRecording(Recording recording)
        {
            using (ResurecDbContext context = _dbContextFactory.CreateDbContext())
            {
                RecordingDTO recordingDTO = ToRecordingDTO(recording);

                context.Recordings!.Add(recordingDTO);
                await context.SaveChangesAsync();
            }
        }

        private static HardwareReportDTO ToHardwareReportDTO(AveragedHardwareReport averagedHardwareReport)
        {
            return new HardwareReportDTO()
            {
                CpuUsage = averagedHardwareReport.CpuUsage,
                CpuTemperature = averagedHardwareReport.CpuTemperature,
                RamUsage = averagedHardwareReport.RamUsage,
                GpuUsage = averagedHardwareReport.GpuUsage,
                GpuTemperature = averagedHardwareReport.GpuTemperature
            };
        }

        private static RecordingDTO ToRecordingDTO(Recording recording)
        {
            return new RecordingDTO()
            {
                StartTime = recording.StartTime,
                EndTime = recording.EndTime,
                HardwareReport = ToHardwareReportDTO(recording.AveragedHardwareReport)
            };
        }
    }
}
