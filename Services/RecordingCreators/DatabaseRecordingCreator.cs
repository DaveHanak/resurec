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

        private static RecordingDTO ToRecordingDTO(Recording recording)
        {
            return new RecordingDTO()
            {
                Id = recording.Id,
                Name = recording.Name,
                StartTime = recording.StartTime,
                EndTime = recording.EndTime,

                CpuUsage = recording.AveragedHardwareReport.CpuUsage,
                CpuTemperature = recording.AveragedHardwareReport.CpuTemperature,
                RamUsage = recording.AveragedHardwareReport.RamUsage,
                GpuUsage = recording.AveragedHardwareReport.GpuUsage,
                GpuTemperature = recording.AveragedHardwareReport.GpuTemperature
            };
        }
    }
}
