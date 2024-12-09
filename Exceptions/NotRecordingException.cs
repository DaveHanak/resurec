using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.Exceptions
{
    public class NotRecordingException : Exception
    {
        public NotRecordingException() : base("Not recording.")
        {
        }
    }
}
