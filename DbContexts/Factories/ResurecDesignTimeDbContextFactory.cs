using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace resurec.DbContexts.Factories
{
    public class ResurecDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ResurecDbContext>
    {
        public ResurecDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=resurec.db").Options;

            return new ResurecDbContext(options);
        }
    }
}
