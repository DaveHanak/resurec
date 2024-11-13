using resurec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Services.RecordingProviders
{
    public class DatabaseRecordingProvider : IRecordingProvider
    {
        public Task<IEnumerable<Recording>> GetRecordings()
        {
            throw new NotImplementedException();
        }
    }
}
