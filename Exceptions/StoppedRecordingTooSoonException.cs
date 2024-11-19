using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Exceptions
{
    public class StoppedRecordingTooSoonException : Exception
    {
        public StoppedRecordingTooSoonException() : base("A recording must be longer than 2 seconds.")
        {
        }
    }
}
