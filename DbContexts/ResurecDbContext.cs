using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using resurec.DTOs;

namespace resurec.DbContexts
{
    public class ResurecDbContext : DbContext
    {
        public ResurecDbContext(DbContextOptions options) : base(options) { }

        public DbSet<RecordingDTO>? Recordings { get; set; }
    }
}
