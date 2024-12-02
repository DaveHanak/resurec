using resurec.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ViewModels.CustomLvc
{
    public interface IUsageDisplay
    {
        public void Update(HardwareReport report);
    }
}
