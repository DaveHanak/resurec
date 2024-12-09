using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace resurec.DbContexts.Factories
{
    public class ResurecDbContextFactory : IResurecDbContextFactory
    {
        private readonly string _connectionString;

        public ResurecDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ResurecDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new ResurecDbContext(options);
        }
    }
}
