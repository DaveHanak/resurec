using resurec.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resurec.ViewModels
{
    public class SoftwareReportViewModel
    {
        private readonly SoftwareReport _softwareReport;

        public SoftwareReportViewModel(SoftwareReport softwareReport)
        {
            _softwareReport = softwareReport;
        }
    }
}
