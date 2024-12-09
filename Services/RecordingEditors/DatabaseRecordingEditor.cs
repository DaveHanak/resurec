using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.DbContexts.Factories;
using resurec.DbContexts;

namespace resurec.Services.RecordingEditors
{
    public class DatabaseRecordingEditor : IRecordingEditor
    {
        private readonly IResurecDbContextFactory _dbContextFactory;

        public DatabaseRecordingEditor(IResurecDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task EditRecording(Guid id, string name)
        {
            using (ResurecDbContext context = _dbContextFactory.CreateDbContext())
            {
                var recording = await context.Recordings!.FindAsync(id);
                if (recording != null)
                {
                    recording.Name = name;
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
