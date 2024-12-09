using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Services.RecordingEditors
{
    public interface IRecordingEditor
    {
        Task EditRecording(Guid id, string name);
    }
}
