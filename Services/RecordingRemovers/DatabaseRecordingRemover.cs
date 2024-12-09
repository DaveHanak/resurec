using Microsoft.EntityFrameworkCore;
using resurec.DbContexts;
using resurec.DbContexts.Factories;
using resurec.DTOs;
using resurec.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Services.RecordingRemovers
{
    public class DatabaseRecordingRemover : IRecordingRemover
    {
        private readonly IResurecDbContextFactory _dbContextFactory;

        public DatabaseRecordingRemover(IResurecDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task RemoveRecording(Guid id)
        {
            using (ResurecDbContext context = _dbContextFactory.CreateDbContext())
            {
                var recording = await context.Recordings!.FindAsync(id);
                if (recording != null)
                {
                    context.Recordings!.Remove(recording);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
