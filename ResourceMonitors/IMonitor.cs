using resurec.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ResourceMonitors
{
    public interface IMonitor<TReport> where TReport : IReport
    {
        TReport CreateReport();
    }
}
