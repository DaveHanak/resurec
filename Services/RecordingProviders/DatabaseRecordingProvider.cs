using Microsoft.EntityFrameworkCore;
using resurec.DbContexts;
using resurec.DbContexts.Factories;
using resurec.DTOs;
using resurec.Models;
using resurec.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Services.RecordingProviders
{
    public class DatabaseRecordingProvider : IRecordingProvider
    {
        private readonly IResurecDbContextFactory _dbContextFactory;

        public DatabaseRecordingProvider(IResurecDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Recording>> GetRecordings()
        {
            using (ResurecDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<RecordingDTO> recordingDTOs = await context.Recordings!.ToListAsync();

                return recordingDTOs.Select(r => ToRecording(r));
            }
        }


        private static Recording ToRecording(RecordingDTO dto)
        {
            return new Recording(
                dto.Id,
                dto.Name,
                dto.StartTime,
                dto.EndTime,
                dto.Duration,
                new AveragedHardwareReport(
                    dto.CpuUsage,
                    dto.CpuTemperature,
                    dto.RamUsage,
                    dto.GpuUsage,
                    dto.GpuTemperature
                )
            );
        }
    }
}
