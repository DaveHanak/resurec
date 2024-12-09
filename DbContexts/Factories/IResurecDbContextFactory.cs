using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.DbContexts.Factories
{
    public interface IResurecDbContextFactory
    {
        ResurecDbContext CreateDbContext();
    }
}
