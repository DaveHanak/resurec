using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resurec.Models;

namespace resurec.Services.RecordingRemovers
{
    public interface IRecordingRemover
    {
        Task RemoveRecording(string recordingName);
    }
}
